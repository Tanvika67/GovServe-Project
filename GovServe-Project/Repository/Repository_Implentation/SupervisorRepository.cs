using GovServe_Project.Models;
using GovServe_Project.Data;
using GovServe_Project.Repository.Interface;
namespace GovServe_Project.Repository.Repository_Implentation
{
	//join application and return that variable it goes to service->controller
	public class SupervisorRepository : ISupervisorRepository
	{
		private readonly GovServe_ProjectContext _context;

		public SupervisorRepository(GovServe_ProjectContext context)
		{
			_context = context;
		}

		public List<Case> GetUnassignedCases()
		{
			return _context.Case
				.Where(c => c.AssignedOfficerId == null && c.Status == "Pending")
				.ToList();
		}

		public List<Case> GetSlaBreachedCases()
		{
			return _context.Case
				.Where(c => c.CompletedDate == null && c.AssignedDate < DateTime.Now)
				.ToList();
		}

		public Case GetCaseById(int caseId)
		{
			return _context.Case.FirstOrDefault(c => c.CaseId == caseId);
		}

		public User GetOfficerById(int officerId)
		{
			return _context.User.FirstOrDefault(u => u.UserId == officerId);
		}

		public void UpdateCase(Case caseData)
		{
			_context.Case.Update(caseData);
			_context.SaveChanges();
		}

		public void CreateEscalation(Escalation escalation)
		{
			_context.Escalation.Add(escalation);
			_context.SaveChanges();
		}

		public List<Escalation> GetEscalations(int caseId)
		{
			return _context.Escalation
				.Where(e => e.CaseId == caseId)
				.ToList();
		}

		public List<Notification> GetNotifications(int userId)
		{
			return _context.Notification
				.Where(n => n.UserId == userId)
				.ToList();
		}

		public void MarkNotificationRead(int notificationId)
		{
			var n = _context.Notification.Find(notificationId);
			n.Status = "Read";
			_context.SaveChanges();
		}
	}
}