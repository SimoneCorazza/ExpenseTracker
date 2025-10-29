using ExpenseTracker.Application.Services.User;
using ExpenseTracker.Domain.Transactions;
using MediatR;

namespace ExpenseTracker.Application.Transactions.Create;

public class CreateTransactionHandler : IRequestHandler<CreateTransactionRequest, CreateTransactionResponse>
{
    private readonly ITransactionRepository transactionRepository;
    private readonly IUser user;

    public CreateTransactionHandler(ITransactionRepository transactionRepository, IUser user)
    {
        this.transactionRepository = transactionRepository;
        this.user = user;
    }

    public async Task<CreateTransactionResponse> Handle(CreateTransactionRequest request, CancellationToken cancellationToken)
    {
        if (user.LoggedUser is null)
        {
            throw new UnauthorizedAccessException("User is not authenticated");
        }

        var transaction = new Transaction(
            user.LoggedUser.UserId,
            request.Amount,
            request.Description,
            request.Date,
            request.CategoryId,
            request.PlaceId,
            []
        );

        transactionRepository.Add(transaction);

        return new CreateTransactionResponse
        {
            Id = transaction.Id
        };
    }
}