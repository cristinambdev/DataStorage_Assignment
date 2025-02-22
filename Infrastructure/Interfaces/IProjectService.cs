using Business.Dtos;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using System.Linq.Expressions;

namespace Business.Interfaces;

public interface IProjectService
{

    Task<IResult> CreateProjectAsync(ProjectRegistrationForm form);
    Task<IResult> GetAllProjectsAsync();

    Task<IResult> GetProjectAsync(Expression<Func<ProjectEntity, bool>> expression);

    Task<IResult> GetProjectByIdAsync(int projectId);

    Task<IResult> UpdateProjectAsync(int id, ProjectUpdateForm form);

    Task<IResult> DeleteProjectAsync(int id);

   
}

