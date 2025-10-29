using ExpenseTracker.Application.Services.User;
using ExpenseTracker.Domain;
using ExpenseTracker.Domain.Transactions;
using MediatR;

namespace ExpenseTracker.Application.Transactions.Edit;

public class EditTransactionHandler : IRequestHandler<EditTransactionRequest>
{
    private readonly ITransactionRepository transactionRepository;
    private readonly IUser user;

    public EditTransactionHandler(ITransactionRepository transactionRepository, IUser user)
    {
        this.transactionRepository = transactionRepository;
        this.user = user;
    }

    public async Task Handle(EditTransactionRequest request, CancellationToken cancellationToken)
    {
        if (user.LoggedUser is null)
        {
            throw new UnauthorizedAccessException("User is not authenticated");
        }

        var transaction = await transactionRepository.GetById(request.Id);
        if (transaction is null)
        {
            throw new DomainException("Transaction not found");
        }

        if (transaction.UserId != user.LoggedUser.UserId)
        {
            throw new UnauthorizedAccessException("User is not authorized to edit this transaction");
        }

        transaction.Update(
            request.Amount,
            request.Description,
            request.Date,
            request.CategoryId,
            request.PlaceId
        );

        await transactionRepository.Update(transaction);
    }
}