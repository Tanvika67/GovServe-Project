using GovServe_Project.Models;

namespace GovServe_Project.Services.Interfaces
{
			public interface ISupervisorService
			{
				List<Case> GetUnassignedCases();
				List<Case> GetSlaBreachedCases();
				string AssignOfficer(int caseId, int officerId);
				string ReassignOfficer(int caseId, int newOfficerId, string reason);
				List<Escalation> GetEscalations(int caseId);
				List<Notification> GetNotifications(int userId);
				string MarkNotificationRead(int notificationId);
			}

}
