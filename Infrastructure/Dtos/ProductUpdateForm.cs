using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public class ProductUpdateForm
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string ProductName { get; set; } = null!;
    
    [Required]
    public decimal Price { get; set; }
}
