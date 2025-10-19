using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Application.Places.Get;

/// <summary>
///     Place
/// </summary>
public class Place
{
    /// <summary>
    ///     Id of the place
    /// </summary>
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    ///     Name of the place
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    ///     Optional description of the place
    /// </summary>
    public string? Description { get; set; }
}