using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;


namespace Business.Services;

public class ProjectService(DataContext context, IProjectRepository projectRepository, ICustomerService customerService) : IProjectService
{
    private readonly ICustomerService _customerService = customerService;
    private readonly IProjectRepository _projectRepository = projectRepository;
    private readonly DataContext _context = context;
  



    public async Task <IResult> CreateProjectAsync(ProjectRegistrationForm form)
    {
        
        if(form == null)
            return Result.BadRequest("Invalid registration form");
        
        //  Check if Customer exists
        var customer = await _customerService.GetCustomerAsync(form.Customer.CustomerName);
        if (customer == null)
        {
            Console.WriteLine();
            Console.WriteLine("Customer not found. Creating new customer: " + form.Customer.CustomerName);
            var result = await _customerService.CreateCustomerAsync(form.Customer);
            if (!result)
            {
                return Result.Error("Failed to create customer. Please try again.");
            }

            customer = await _customerService.GetCustomerAsync(form.Customer.CustomerName);
        }

        //Start database transaction
        await _projectRepository.BeginTransactionAsync();
        try
        {
            //// Check if Project already exists
            //if (await _projectRepository.AlreadyExistsAsync(x => x.Title == form.Title))
            //{
            //    return Result.AlreadyExists("Project with same title already exists");
            //}

            var projectEntity = ProjectFactory.Create(form);
            if (projectEntity == null)
            {
                return Result.Error("Failed to create project.");
            }


            // Save Project
            await _projectRepository.CreateAsync(projectEntity);
            await _projectRepository.SaveAsync();

            
            // Commit transaction
            await _projectRepository.CommitTransactionAsync();
            return Result.OK();
        }
        catch (Exception ex)
        {
            await _projectRepository.RollbackTransactionAsync();
            Debug.WriteLine(ex.Message);
            return Result.Error(ex.Message);
        }
    }
    
    public async Task<IResult> GetAllProjectsAsync()
    {
        var projectEntities = await _projectRepository.GetAllAsync(query =>
            query.Include(project => project.Customer)
                 .Include(project => project.Status)
                 .Include(project => project.User)
                 .Include(project => project.Product)
        );

        if (projectEntities == null)
        {
            return Result<IEnumerable<Project>>.Error("Error fetching projects");
        }

        if (!projectEntities.Any())
        {
            return Result<IEnumerable<Project>>.Error("No projects found");
        }

        var projects = projectEntities.Select(ProjectFactory.Create);
        return Result<IEnumerable<Project>>.Ok(projects);
    }



    public async Task<IResult> GetProjectByIdAsync(int projectId)
    {
        var projectEntity = await _projectRepository.GetAsync(x => x.Id == projectId);

        if (projectEntity == null)
            return Result.NotFound("Project was not found");

        var project = ProjectFactory.Create(projectEntity); 
        return Result<Project>.Ok(project);

    }

    public async Task<IResult> GetProjectAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        var projectEntity = await _projectRepository.GetWithProjectDetailsAsync(expression);

        if (projectEntity == null)
            return Result.NotFound("Project was not found");

        var project = ProjectFactory.Create(projectEntity);
        return Result<Project>.Ok(project);
    }

    public async Task<IResult> UpdateProjectAsync(int id, ProjectUpdateForm form)
    {
        try
        {
            await _projectRepository.BeginTransactionAsync();

            var projectEntity = await _projectRepository.GetAsync(x => x.Id == id);

            if (projectEntity == null)
                return Result.NotFound("Project was not found");


            //code with help of Chat GPT
            if (!string.IsNullOrEmpty(form.Title))
                projectEntity.Title = form.Title;

            if (!string.IsNullOrEmpty(form.Description))
                projectEntity.Description = form.Description;

            // Fetch all StatusType entities into memory and perform the comparison in code.
            var status = await _context.StatusTypes
                .ToListAsync(); // Fetch all StatusTypes

            var foundStatus = status.FirstOrDefault(s => s.Status.Equals(form.Status, StringComparison.OrdinalIgnoreCase));

            if (foundStatus != null)
                projectEntity.Status = foundStatus;


            if (!string.IsNullOrEmpty(form.Customer))
            {
                var customer = await _context.Customers
                    .FirstOrDefaultAsync(c => c.CustomerName == form.Customer);
                if (customer != null)
                {
                    projectEntity.Customer = customer;
                    _context.Entry(customer).State = EntityState.Modified; // Mark Customer as modified
                }
            }

            // Update product if applicable
            if (!string.IsNullOrEmpty(form.ProductName))
            {
                var product = await _context.Products
                    .FirstOrDefaultAsync(p => p.ProductName == form.ProductName);
                if (product != null)
                {
                    projectEntity.Product = product;
                    _context.Entry(product).State = EntityState.Modified; // Mark Product as modified
                }
            }

            if (!string.IsNullOrEmpty(form.UserFirstName) && projectEntity.User != null)
                projectEntity.User.FirstName = form.UserFirstName;

            if (!string.IsNullOrEmpty(form.UserLastName) && projectEntity.User != null)
                projectEntity.User.LastName = form.UserLastName;

            if (form.StartDate != DateTime.MinValue)
                projectEntity.StartDate = form.StartDate;

            if (form.EndDate != DateTime.MinValue)
                projectEntity.EndDate = form.EndDate;

            if (form.ProductPrice > 0 && projectEntity.Product != null)
            {
                projectEntity.Product.Price = form.ProductPrice;
                _context.Entry(projectEntity.Product).State = EntityState.Modified;
            }

            //up to here code with the help of Chat GPT


            var changes = await _context.SaveChangesAsync();
            await _projectRepository.CommitTransactionAsync();

            return changes > 0 ? Result.OK() : Result.Error("No changes were made.");
        }
        catch (Exception ex)
        {
            await _projectRepository.RollbackTransactionAsync();
            return Result.Error("Error updating project");
        }

    }


    public async Task<IResult> DeleteProjectAsync(int id)
    {
        await _projectRepository.BeginTransactionAsync();

        try
        {
            var existingProject = await _projectRepository.GetAsync(x => x.Id == id);
            if (existingProject == null)
                return Result.NotFound("Project was not found");

            var result = await _projectRepository.DeleteAsync(existingProject);
            
            await _projectRepository.CommitTransactionAsync();

            return result ? Result.OK() : Result.Error("Unable to delete project");
        }
        catch
        {
            await _projectRepository.RollbackTransactionAsync();
            return Result.Error("Error deleting project");

        }

    }
}
