using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public class ProjectUpdateForm
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    public string Status { get; set; } = null!;

    [Required]
    public string UserFirstName { get; set; } = null!;

    [Required]
    public string UserLastName { get; set; } = null!;

    [Required]
    public string Customer { get; set; } = null!;
    [Required]
    public string ProductName { get; set; } = null!;
    [Required]
    public decimal ProductPrice { get; set; }

   

}

