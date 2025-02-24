using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public class ProjectRegistrationForm
{
 
    public string Title { get; set; } = null!;
    public string? Description { get; set; }

    public DateTime StartDate { get; set; }

    
    public DateTime EndDate { get; set; }

    public CustomerRegistrationForm Customer { get; set; } = null!;


    public StatusTypeRegistrationForm Status { get; set; } = null!;

  
    public UserRegistrationForm User { get; set; } = null!;


    public ProductRegistrationForm Product { get; set; } = null!;

}



