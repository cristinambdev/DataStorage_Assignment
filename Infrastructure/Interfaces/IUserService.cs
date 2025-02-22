using Business.Dtos;
using Business.Models;

namespace Business.Interfaces
{
    public interface IUserService
    {
        Task<bool> CreateUserAsync(UserRegistrationForm form);
        Task DeleteUserAsync(int id);
        Task<User?> GetUserAsync(int id);
        Task<User?> GetUserAsync(string userName);
        Task<IEnumerable<User?>> GetUsersAsync();
        Task UpdateUserAsync(UserUpdateForm user);
        Task<bool> UserExistsAsync(string userName);
    }
}