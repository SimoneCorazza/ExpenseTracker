using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Application.Places.Edit;

/// <summary>
///     Edit the given place
/// </summary>
public class EditPlace : IRequest
{
    /// <summary>
    ///     Id of the place to update
    /// </summary>
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    ///     New name of the place
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    ///     Optional new description of the place
    /// </summary>
    public string? Description { get; set; }
}