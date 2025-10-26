using ExpenseTracker.Application.Services.ObjectStorage;
using ExpenseTracker.Domain;
using ExpenseTracker.Domain.Transactions;
using ExpenseTracker.Domain.Transactions.Services.TransactionAttachment;
using MediatR;
using Minio.Exceptions;

namespace ExpenseTracker.Application.Transactions.Attachments.Add;

public class AddAttachmentsHandler : IRequestHandler<AddAttachmentsRequest>
{
    private readonly ITransactionRepository transactionRepository;
    private readonly ITransactionAttachmentService transactionAttachmentService;
    private readonly IObjectStorage objectStorage;

    public AddAttachmentsHandler(
        ITransactionRepository transactionRepository,
        ITransactionAttachmentService transactionAttachmentService,
        IObjectStorage objectStorage)
    {
        this.transactionRepository = transactionRepository;
        this.transactionAttachmentService = transactionAttachmentService;
        this.objectStorage = objectStorage;
    }

    public async Task Handle(AddAttachmentsRequest request, CancellationToken cancellationToken)
    {
        var transaction = await transactionRepository.GetById(request.TransactionId)
            ?? throw new DomainException("Transaction not found");

        if (request.Attachments.Any(x => x.Data.Length > transactionAttachmentService.MaxSizeInBytes))
        {
            throw new DomainException("One or more attachments exceed the maximum allowed size");
        }
        else if (request.Attachments.Any(x => !transactionAttachmentService.ValidateFromContent(x.Data)))
        {
            throw new DomainException("One or more attachments have an invalid file type");
        }

        // TODO: add Polly
        List<Guid> attachmentIds = new(request.Attachments.Count);
        await Parallel.ForEachAsync(request.Attachments, async (x, _) =>
        {
            var id = Guid.NewGuid();
            await objectStorage.Upload(
                x.Data,
                id,
                x.MimeType,
                // Tag to reverse search this attachment (eg to delete it later):
                new Dictionary<string, string> { {"TransactionId", request.TransactionId.ToString()} });
            attachmentIds.Add(id);
        });

        var attachments = request.Attachments.Select((x, i) => new Attachment(
            attachmentIds[i],
            x.Name,
            x.Description,
            x.MimeType,
            x.Data.Length))
            .ToList();

        transaction.Update(attachments, transactionAttachmentService.MaxCount);

        try
        {
            await transactionRepository.Save(transaction);
        }
        catch
        {
            // Rollback uploaded attachments in case of error
            try
            {
                // TODO: add Polly
                await Parallel.ForEachAsync(attachmentIds, async (id, _) =>
                {
                    await objectStorage.Delete(id);
                });
            }
            catch
            {
                // Avoid masking the original exception
            }

            throw;
        }
    }
}
