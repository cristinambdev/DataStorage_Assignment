using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories;

public class ProjectRepository(DataContext context) : BaseRepository<ProjectEntity>(context), IProjectRepository
{
    public async Task<ProjectEntity?> GetWithProjectDetailsAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        return await _dbSet
            .Include(project => project.Customer)
            .Include(project => project.Status)
            .Include(project => project.User)
            .Include(project => project.Product)
            .FirstOrDefaultAsync(expression);
    }

   

}
