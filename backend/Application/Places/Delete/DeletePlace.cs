using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Application.Places.Delete;

/// <summary>
///     Delete the given place
/// </summary>
public class DeletePlace : IRequest
{
    [Required]
    public Guid Id { get; set; }
}