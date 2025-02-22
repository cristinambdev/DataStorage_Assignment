using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public class ProjectRegistrationForm
{
    [Required]
    public string Title { get; set; } = null!;
    public string? Description { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    public StatusTypeRegistrationForm Status { get; set; } = null!;

    [Required]
    public UserRegistrationForm User { get; set; } = null!;

    [Required]
    public CustomerRegistrationForm Customer { get; set; } = null!;

    [Required]
    public ProductRegistrationForm Product { get; set; } = null!;
    

}
