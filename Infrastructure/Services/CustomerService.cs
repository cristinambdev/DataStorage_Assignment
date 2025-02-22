using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;



namespace Business.Services;

public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
{

    private readonly ICustomerRepository _customerRepository = customerRepository;

    //CREATE
    public async Task<bool> CreateCustomerAsync(CustomerRegistrationForm form)
    {
        if (await _customerRepository.AlreadyExistsAsync(x => x.CustomerName == form.CustomerName))
            return false;

        await _customerRepository.BeginTransactionAsync();


        try
        {
            
            await _customerRepository.CreateAsync(new CustomerEntity { CustomerName = form.CustomerName});
            await _customerRepository.SaveAsync();

            await _customerRepository.CommitTransactionAsync();
            return true;
        }
        catch
        {
            await _customerRepository.RollbackTransactionAsync();
            return false;
        }
    }


    //READ
    public async Task<IEnumerable<Customer?>> GetCustomersAsync()
    {
        var customerEntities = await _customerRepository.GetAllAsync();
        return customerEntities.Select(CustomerFactory.Create);
    }

    public async Task<Customer?> GetCustomerAsync(int id)
    {
        var customerEntity = await _customerRepository.GetAsync(x => x.Id == id);
        return CustomerFactory.Create(customerEntity!);
    }

    public async Task<Customer?> GetCustomerAsync(string customerName)
    {
        var customerEntity = await _customerRepository.GetAsync(x => x.CustomerName == customerName);
        return CustomerFactory.Create(customerEntity!);
    }

    //UPDATE
    public async Task UpdateCustomerAsync(CustomerUpdateForm customer)
    {
        await _customerRepository.BeginTransactionAsync();

        try
        {
            var existingCustomer = await _customerRepository.GetAsync(x => x.Id == customer.Id);
            if (existingCustomer == null)
            {
                throw new Exception("Customer not found");
            }

            existingCustomer = CustomerFactory.Update(customer);

            await _customerRepository.UpdateAsync(existingCustomer!);
            await _customerRepository.CommitTransactionAsync();

        }

        catch
        {
            await _customerRepository.RollbackTransactionAsync();
        }
    }

    //DELETE
    public async Task DeleteCustomerAsync(int id)
    {
        await _customerRepository.BeginTransactionAsync();

        try
        {
            var existingCustomer = await _customerRepository.GetAsync(x => x.Id == id);
            if (existingCustomer == null)
            {
                throw new Exception("Customer not found");
            }

            await _customerRepository.DeleteAsync(existingCustomer);
            await _customerRepository.CommitTransactionAsync();
        }
        catch
        {
            await _customerRepository.RollbackTransactionAsync();
        }

    }

    public async Task<bool> CustomerExistsAsync(string CustomerName)
    {
        var result = await _customerRepository.AlreadyExistsAsync(x => x.CustomerName == CustomerName);
        return result;
    }

    
}
