using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace Business.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;

    //CREATE
    public async Task<bool> CreateUserAsync(UserRegistrationForm form)
    {
        if (await _userRepository.AlreadyExistsAsync(x => x.FirstName == form.FirstName && x.LastName == form.LastName))
            return false;

        await _userRepository.BeginTransactionAsync();

        try
        {
            await _userRepository.CreateAsync(UserFactory.Create(form)!);
            await _userRepository.SaveAsync();
            Console.WriteLine("User saved successfully");
            await _userRepository.CommitTransactionAsync();

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            await _userRepository.RollbackTransactionAsync();
            return false;
        }

    }

    //READ
    public async Task<IEnumerable<User?>> GetUsersAsync()
    {
        var usersEntities = await _userRepository.GetAllAsync();
        return usersEntities.Select(entity => UserFactory.Create(entity));
    }

    public async Task<User?> GetUserAsync(int id)
    {
        var userEntity = await _userRepository.GetAsync(x => x.Id == id);
        return UserFactory.Create(userEntity!);
    }

    public async Task<User?> GetUserAsync(string userName)
    {
        if (string.IsNullOrEmpty(userName))
        {
            throw new ArgumentException("Username cannot be null or empty", nameof(userName));
        }

        var userEntity = await _userRepository.GetAsync(u => u.FirstName == userName || u.LastName == userName);

        return userEntity != null ? UserFactory.Create(userEntity) : null;
    }


    //UPDATE

    public async Task UpdateUserAsync(UserUpdateForm user)
    {
        await _userRepository.BeginTransactionAsync();

        try
        {
            var existingUser = await _userRepository.GetAsync(x => x.Id == user.Id);
            if (existingUser == null)
            {
                throw new Exception("User not found");
            }
            existingUser = UserFactory.Update(user);
            await _userRepository.UpdateAsync(existingUser);
            await _userRepository.CommitTransactionAsync();
        }
        catch
        {
            await _userRepository.RollbackTransactionAsync();
        }
    }

    //DELETE

    public async Task DeleteUserAsync(int id)
    {
        await _userRepository.BeginTransactionAsync();

        try
        {
            var existingUser = await _userRepository.GetAsync(x => x.Id == id);
            if (existingUser == null)
            {
                throw new Exception("User not found");
            }

            await _userRepository.DeleteAsync(existingUser);
            await _userRepository.CommitTransactionAsync();
        }

        catch
        {
            await _userRepository.RollbackTransactionAsync();
        }
    }


    public async Task<bool> UserExistsAsync(string userName)
    {
        var result = await _userRepository.AlreadyExistsAsync(x => x.FirstName == userName);
        return result;
    }

}


