using ExpenseTracker.Application.Services.User;
using ExpenseTracker.Domain;
using ExpenseTracker.Domain.Transactions;
using MediatR;

namespace ExpenseTracker.Application.Transactions.Delete;

public class DeleteTransactionHandler : IRequestHandler<DeleteTransactionRequest>
{
    private readonly ITransactionRepository transactionRepository;
    private readonly IUser user;

    public DeleteTransactionHandler(ITransactionRepository transactionRepository, IUser user)
    {
        this.transactionRepository = transactionRepository;
        this.user = user;
    }

    public async Task Handle(DeleteTransactionRequest request, CancellationToken cancellationToken)
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
            throw new UnauthorizedAccessException("User is not authorized to delete this transaction");
        }

        await transactionRepository.Delete(transaction);

        // TODO: Consider deleting attachments from storage as well
    }
}