using ExpenseTracker.Application.Services.ObjectStorage;
using ExpenseTracker.Domain;
using ExpenseTracker.Domain.Transactions;
using ExpenseTracker.Domain.Transactions.Services.TransactionAttachment;
using MediatR;

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
        
        Guid[] attachmentIds = new Guid[request.Attachments.Count];
        Parallel.For(0, request.Attachments.Count, async i =>
        {
            var id = Guid.NewGuid();
            attachmentIds[i] = id;
            await objectStorage.Upload(request.Attachments[i].Data, id);
        });

        if (request.Attachments.Any(x => x.Data.Length > transactionAttachmentService.MaxSizeInBytes))
        {
            throw new DomainException("One or more attachments exceed the maximum allowed size");
        }
        else if (request.Attachments.Any(x => !transactionAttachmentService.ValidateFromContent(x.Data)))
        {
            throw new DomainException("One or more attachments have an invalid file type");
        }

        var attachments = request.Attachments.Select((x, i) => new Attachment(
            attachmentIds[i],
            x.Name,
            x.Description,
            x.MimeType,
            x.Data.Length))
            .ToList();

            transaction.Update(attachments, transactionAttachmentService.MaxCount);
        await transactionRepository.Save(transaction);
    }
}
