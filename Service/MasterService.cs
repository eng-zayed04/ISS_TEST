using ISS_TEST.Models;
using ISS_TEST.Repository;

namespace ISS_TEST.Service
{
    public class MasterService : IMasterService
    {
        private readonly IMasterRepository _repository;

        public MasterService(IMasterRepository repository)
        {
            _repository = repository;
        }

        public Task<List<MasterModel>> GetAllAsync() => _repository.GetAllAsync();
        public Task<MasterModel?> GetByIdAsync(long id) => _repository.GetByIdAsync(id);
        public Task<bool> InsertAsync(MasterModel model) => _repository.InsertAsync(model);
        public Task<bool> UpdateAsync(MasterModel model) => _repository.UpdateAsync(model);
        public Task<bool> DeleteAsync(long id) => _repository.DeleteAsync(id);
    }
}
