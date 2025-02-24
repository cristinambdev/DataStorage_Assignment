using Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.Models;

public class Project
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    [Column(TypeName = "date")]
    public DateTime StartDate { get; set; }

    [Column(TypeName = "date")]
    public DateTime EndDate { get; set; }


    public int CustomerId { get; set; }
    public CustomerEntity Customer { get; set; } = null!;


    public int StatusId { get; set; }
    public StatusTypeEntity Status { get; set; } = null!;

    public int UserId { get; set; }
    public UserEntity User { get; set; } = null!;


    public int ProductId { get; set; }
    public ProductEntity Product { get; set; } = null!;
}

//public class Project
//{
//    public int Id { get; set; }

//    public string Title { get; set; } = null!;

//    public string? Description { get; set; }

//    public DateTime StartDate { get; set; }
//    public DateTime EndDate { get; set; }

//    public string Status { get; set; } = null!;

//    public string UserFirstName { get; set; } = null!;

//    public string UserLastName { get; set; } = null!;

//    public string Customer { get; set; } = null!;

//    public string ProductName { get; set; } = null!;

//    public decimal ProductPrice { get; set; }

//}
