
using GovServe_Project.Models.AdminModels;

namespace GovServe_Project.Repository.Interface.AdminRepositoryInterface
{
    public interface IRequiredDocumentRepository
    {
        Task<IEnumerable<RequiredDocument>> GetAllAsync();
        Task<RequiredDocument?> GetByIdAsync(int id);
        Task AddAsync(RequiredDocument document);
        Task UpdateAsync(RequiredDocument document);
        Task DeleteAsync(RequiredDocument document);
    }
}
