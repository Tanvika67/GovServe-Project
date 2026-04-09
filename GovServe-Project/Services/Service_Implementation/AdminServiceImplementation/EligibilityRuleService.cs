using GovServe_Project.DTOs.Admin;
using GovServe_Project.Enum;
using GovServe_Project.Exceptions;
using GovServe_Project.Models.AdminModels;
using GovServe_Project.Repository.Interface.AdminRepositoryInterface;
using GovServe_Project.Repository.Repository_Implentation.AdminRepositoryImplementation;
using GovServe_Project.Services.Interfaces.AdminServiceInterface;

namespace GovServe_Project.Services.Service_Implementation.AdminServiceImplementation
{
    public class EligibilityRuleService : IEligibilityRuleService
    {
        private readonly IEligibilityRuleRepository _repository;
       

        public EligibilityRuleService(IEligibilityRuleRepository repository)
        {
            _repository = repository;
            
        }

        public async Task<IEnumerable<EligibilityRuleResponseDTO>> GetAllAsync()
        {
            var rules = await _repository.GetAllAsync();

            return rules.Select(r => new EligibilityRuleResponseDTO
            {
                RuleID = r.RuleID,
                ServiceName = r.ServiceName ?? "",
                RuleDescription = r.RuleDescription,
                RuleExpression = r.RuleExpression
            });
        }

        public async Task<EligibilityRuleResponseDTO> GetByIdAsync(int id)
        {
            var rule = await _repository.GetByIdAsync(id);

            if (rule == null)
                throw new NotFoundException("Eligibility rule not found.");

            return new EligibilityRuleResponseDTO
            {
                RuleID = rule.RuleID,
                ServiceName = rule.ServiceName ?? "",
                RuleDescription = rule.RuleDescription,
                RuleExpression = rule.RuleExpression
            };
        }

        public async Task<EligibilityRuleResponseDTO> CreateAsync(EligibilityRuleDTO dto)
        {
           

            var rule = new EligibilityRule
            {
                ServiceID = dto.ServiceID,
                RuleDescription = dto.RuleDescription,
                RuleExpression = dto.RuleExpression
            };

            await _repository.AddAsync(rule);

            return await GetByIdAsync(rule.RuleID);
        }

        public async Task<EligibilityRuleResponseDTO> UpdateAsync(int id, EligibilityRuleDTO dto)
        {
            var rule = await _repository.GetByIdAsync(id);

            if (rule == null)
                throw new NotFoundException("Eligibility rule not found.");

            rule.ServiceID = dto.ServiceID;
            rule.RuleDescription = dto.RuleDescription;
            rule.RuleExpression = dto.RuleExpression;

            await _repository.UpdateAsync(rule);

            return await GetByIdAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            var rule = await _repository.GetByIdAsync(id);

            if (rule == null)
                throw new NotFoundException("Eligibility rule not found.");

            await _repository.DeleteAsync(rule);
        }

        //Search eligibility rules by service name
        public async Task<IEnumerable<EligibilityRuleResponseDTO>> SearchByServiceNameAsync(string serviceName)
        {
            var rules = await _repository.GetByServiceNameAsync(serviceName);

            if (!rules.Any())
                throw new NotFoundException("No eligibility rules found for this service.");

            return rules.Select(r => new EligibilityRuleResponseDTO
            {
                RuleID = r.RuleID,
                ServiceName = r.ServiceName ?? "",
                RuleDescription = r.RuleDescription,
                RuleExpression = r.RuleExpression
            });
        
        }

		//for citizen
		public async Task<IEnumerable<EligibilityRuleResponseDTO>> GetByServiceIdAsync(int serviceId)
		{
			var rules = await _repository.GetByServiceIdAsync(serviceId);

			if (!rules.Any())
				throw new NotFoundException("No eligibility rules found for this service.");

			return rules.Select(r => new EligibilityRuleResponseDTO
			{
				RuleID = r.RuleID,
				ServiceName = r.Service?.ServiceName ?? "",
				RuleDescription = r.RuleDescription,
				RuleExpression = r.RuleExpression
			});
		}
	}
}
