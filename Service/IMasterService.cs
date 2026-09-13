using ISS_TEST.Models;

namespace ISS_TEST.Service
{
    public interface IMasterService
    {

        Task<List<MasterModel>> GetAllAsync();
        Task<MasterModel?> GetByIdAsync(long id);
        Task<bool> InsertAsync(MasterModel model);
        Task<bool> UpdateAsync(MasterModel model);
        Task<bool> DeleteAsync(long id);
    }
}
