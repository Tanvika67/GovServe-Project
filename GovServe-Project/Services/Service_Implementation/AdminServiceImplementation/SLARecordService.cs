
using GovServe_Project.DTOs.AdminDTO;
using GovServe_Project.Exceptions;
using GovServe_Project.Models.AdminModels;
using GovServe_Project.Repository.Interface.AdminRepositoryInterface;
using GovServe_Project.Services.Interfaces.AdminServiceInterface;

namespace GovServe_Project.Services.Service_Implementation.AdminServiceImplementation
{ 
  public class SLARecordService : ISLARecordService
    {
        private readonly ISLARecordRepository _repository;

        public SLARecordService(ISLARecordRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SLARecordResponseDTO>> GetAllAsync()
        {
            var record = await _repository.GetAllAsync();

            return record.Select(s => new SLARecordResponseDTO
            {
        
                  SLARecordID=s.SLARecordID,
                  CaseID=s.CaseID,
                  StageID=s.StageID,
                  StartDate = s.StartDate,
                  EndDate = s.EndDate,
                  Status = s.Status


            });
        }

        public async Task<SLARecordResponseDTO> GetByIdAsync(int id)
        {
            var record = await _repository.GetByIdAsync(id);

                if(record==null)
                 throw new NotFoundException("SLARecord not found");

            return new SLARecordResponseDTO
            {

                SLARecordID = record.SLARecordID,
                CaseID = record.CaseID,
                StageID = record.StageID,
                StartDate = record.StartDate,
                EndDate = record.EndDate,
                Status = record.Status

            };
        }

        public async Task<SLARecordResponseDTO> CreateAsync(SLARecordDTO dto)
        {
            var record = new SLARecord
            {
                CaseID = dto.CaseID,
                StageID = dto.StageID,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Status = dto.Status
            };

            await _repository.AddAsync(record);

            return await GetByIdAsync(record.SLARecordID);

            
        }

        public async Task<SLARecordResponseDTO> UpdateAsync(int id, SLARecordDTO dto)
        {
            var record = await _repository.GetByIdAsync(id);
            if(record==null)
             throw new NotFoundException("SLARecord not found");

            record.CaseID = dto.CaseID;
            record.StageID = dto.StageID;
            record.StartDate = dto.StartDate;
            record.EndDate = dto.EndDate;
            record.Status = dto.Status;


            await _repository.UpdateAsync(record);

            return await GetByIdAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            var record = await _repository.GetByIdAsync(id);
            if(record==null)
            throw new NotFoundException("SLARecord not found");

           // _repository.Delete(record);
            await _repository.DeleteAsync(record);
        }
    }


}
