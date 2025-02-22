using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;

namespace Business.Services;

public class StatusTypeService(IStatusTypeRepository statusTypeRepository) : IStatusTypeService
{
    private readonly IStatusTypeRepository _statusTypeRepository = statusTypeRepository;

    //Create
    public async Task<bool> CreateStatusTypeAsync(StatusTypeRegistrationForm form)
    {
        if (await _statusTypeRepository.AlreadyExistsAsync(x => x.Status == form.Status))
            return false;

        await _statusTypeRepository.BeginTransactionAsync();

        try
        {
            await _statusTypeRepository.CreateAsync(StatusTypeFactory.Create(form));
            await _statusTypeRepository.SaveAsync();
            await _statusTypeRepository.CommitTransactionAsync();
            return true;
        }

        catch
        {
            await _statusTypeRepository.RollbackTransactionAsync();
            return false;
        }


    }

    //Read
    public async Task<IEnumerable<StatusTypeModel?>> GetStatusTypesAsync()
    {
        var statusTypeEntities = await _statusTypeRepository.GetAllAsync();

        return statusTypeEntities.Select(entity => StatusTypeFactory.Create(entity));
    }

    public async Task<StatusTypeModel?> GetStatusTypeAsync(int id)
    {
        var statusTypeEntity = await _statusTypeRepository.GetAsync(x => x.Id == id);
        if (statusTypeEntity == null) //Chat gpt
        {
             //Handle the case where the entity is not found
            throw new InvalidOperationException($"StatusType with id {id} not found.");
        }
        return StatusTypeFactory.Create(statusTypeEntity);
    }

    public async Task<StatusTypeModel?> GetStatusTypeAsync(string statusType)
    {
        var statusTypeEntity = await _statusTypeRepository.GetAsync(x => x.Status == statusType);
        return StatusTypeFactory.Create(statusTypeEntity!); 
    }


    //Update

    public async Task UpdateStatusTypeAsync(StatusTypeUpdateForm form)
    {
        await _statusTypeRepository.BeginTransactionAsync();

        try
        {
            var existingStatusType = await _statusTypeRepository.GetAsync(x => x.Id == form.Id);
            if (existingStatusType == null)
            {
                throw new Exception("Status Type not found");
            }

            existingStatusType = StatusTypeFactory.Update(form);
            await _statusTypeRepository.UpdateAsync(existingStatusType!);

            await _statusTypeRepository.CommitTransactionAsync();
        }

        catch
        {
            await _statusTypeRepository.RollbackTransactionAsync();

        }
    }

    //Delete

    public async Task DeleteStatusTypeAsync(int id)
    {
        await _statusTypeRepository.BeginTransactionAsync();

        try
        {
            var existingStatusType = await _statusTypeRepository.GetAsync(x => x.Id == id);
            if (existingStatusType == null)
            {
                throw new Exception("Status Type not found");
            }
           await _statusTypeRepository.DeleteAsync(existingStatusType);
            await _statusTypeRepository.CommitTransactionAsync();

        }
        catch
        {
            await _statusTypeRepository.RollbackTransactionAsync();
        }

    }
}
