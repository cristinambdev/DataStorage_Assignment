using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public class StatusTypeUpdateForm
{

    [Required]
    public int Id { get; set; }

    [Required]
    public string Status { get; set; } = null!;
}
