using Business.Dtos;
using Business.Models;
using Data.Entities;
using System.ComponentModel;

namespace Business.Factories;

public static class ProjectFactory
{
    public static ProjectRegistrationForm Create() => new();
    public static ProjectUpdateForm CreateUpdateForm() => new();


    public static ProjectEntity Create(ProjectRegistrationForm form) => new()
    {
        Title = form.Title,
        Description = form.Description,
        StartDate = form.StartDate,
        EndDate = form.EndDate,

        User = new UserEntity
        {
            FirstName = form.User.FirstName,
            LastName = form.User.LastName,
            Email = form.User.Email!
        },
        Product = new ProductEntity
        {
            ProductName = form.Product.ProductName,
            Price = form.Product.Price
        },
        Customer = new CustomerEntity

        { CustomerName = form.Customer.CustomerName },

        Status = new StatusTypeEntity
        {
            Status = form.Status.Status
        }

    };


    public static Project Create(ProjectEntity entity) => new()
    {


        Id = entity.Id,
        Title = entity.Title,
        Description = entity.Description,
        StartDate = entity.StartDate,
        EndDate = entity.EndDate,

        User = new UserEntity
        {
            FirstName = entity.User.FirstName,
            LastName = entity.User.LastName,
            Email = entity.User.Email!
        },
        Product = new ProductEntity
        {
            ProductName = entity.Product.ProductName,
            Price = entity.Product.Price
        },
        Customer = new CustomerEntity

        { CustomerName = entity.Customer.CustomerName },

        Status = new StatusTypeEntity
        {
            Status = entity.Status.Status
        }

    };



    public static ProjectEntity Update(int id, ProjectUpdateForm form) => new()
    {
        Id = id,
        Title = form.Title,
        Description = form.Description,
        StartDate = form.StartDate,
        EndDate = form.EndDate,
        User = new UserEntity
        {
            FirstName = form.UserFirstName,
            LastName = form.UserLastName
        },
        Product = new ProductEntity
        {
            ProductName = form.ProductName,
            Price = form.ProductPrice
        },
        Customer = new CustomerEntity
        {
            CustomerName = form.Customer
        },
        Status = new StatusTypeEntity
        {
            Status = form.Status
        }

    };
}