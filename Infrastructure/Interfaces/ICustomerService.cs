using Business.Dtos;
using Business.Models;

namespace Business.Interfaces
{
    public interface ICustomerService
    {
        Task <bool>CreateCustomerAsync(CustomerRegistrationForm form);
        Task<bool> CustomerExistsAsync(string CustomerName);
        Task DeleteCustomerAsync(int id);
        Task<Customer?> GetCustomerAsync(int id);
        Task<Customer?> GetCustomerByNameAsync(string customerName);
        Task<IEnumerable<Customer?>> GetCustomersAsync();
        Task UpdateCustomerAsync(CustomerUpdateForm customer);

    }
}