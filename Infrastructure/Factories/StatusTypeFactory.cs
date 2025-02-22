using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public class StatusTypeFactory
{
    public static StatusTypeEntity Create(StatusTypeRegistrationForm form) 
    {
        

    return new StatusTypeEntity
    {
        Status = form.Status,
    };
}
    

    public static StatusTypeModel Create(StatusTypeEntity? entity)
    {
      

        return new StatusTypeModel
        {
            Id = entity!.Id,
            Status = entity.Status,
        };
    }


    public static StatusTypeEntity Update(StatusTypeUpdateForm entity) => new()  
    {
        Id = entity.Id,
        Status = entity.Status,
    };
}
