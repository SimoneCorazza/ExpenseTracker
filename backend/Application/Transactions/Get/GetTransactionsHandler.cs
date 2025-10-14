using ExpenseTracker.Application.Services.User;
using ExpenseTracker.Domain.Transactions;
using MediatR;

namespace ExpenseTracker.Application.Transactions.Get;

public class GetTransactionsHandler : IRequestHandler<GetTransactionsRequest, GetTransactionsResponse>
{
    private readonly ITransactionRepository transactionRepository;
    private readonly IUser user;

    public GetTransactionsHandler(ITransactionRepository transactionRepository, IUser user)
    {
        this.transactionRepository = transactionRepository;
        this.user = user;
    }

    public async Task<GetTransactionsResponse> Handle(GetTransactionsRequest request, CancellationToken cancellationToken)
    {
        if (user.LoggedUser is null)
        {
            throw new UnauthorizedAccessException("User is not authenticated");
        }

        var transactions = await transactionRepository.GetFor(user.LoggedUser.UserId);

        return new GetTransactionsResponse
        {
            Transactions = transactions.Select(t => new TransactionDto
            {
                Id = t.Id,
                Amount = t.Amount,
                Description = t.Description,
                Date = t.Date,
                CategoryId = t.CategoryId,
                PlaceId = t.PlaceId,
                Attachments = t.Attachments.Select(a => new AttachmentDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    MimeType = a.MimeType,
                    Size = a.Size,
                }).ToList()
            }).ToList()
        };
    }
}