using Business.Dtos;
using Business.Models;

namespace Business.Interfaces
{
    public interface IStatusTypeService
    {
        Task<bool> CreateStatusTypeAsync(StatusTypeRegistrationForm form);
        Task DeleteStatusTypeAsync(int id);
        Task<StatusTypeModel?> GetStatusTypeAsync(int id);
        Task<StatusTypeModel?> GetStatusTypeAsync(string statusType);
        Task<IEnumerable<StatusTypeModel?>> GetStatusTypesAsync();
        Task UpdateStatusTypeAsync(StatusTypeUpdateForm form);
    }
}