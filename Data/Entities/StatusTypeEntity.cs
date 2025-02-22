using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;



public class StatusTypeEntity
{

    [Key]
    public int Id { get; set; }

    [Required]
    public string Status { get; set; } = null!;

    public ICollection<ProjectEntity> Projects { get; set; } = [];
}



