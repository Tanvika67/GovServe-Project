using GovServe_Project.Models;

namespace GovServe_Project.Repository.Interface
{
			public interface ISupervisorRepository
			{
				List<Case> GetUnassignedCases();
				List<Case> GetSlaBreachedCases();
				Case GetCaseById(int caseId);
		        User GetOfficerById(int officerId);
				void UpdateCase(Case caseData);
				void CreateEscalation(Escalation escalation);
				List<Escalation> GetEscalations(int caseId);
				List<Notification> GetNotifications(int userId);
				void MarkNotificationRead(int notificationId);
			}
		
	
}
