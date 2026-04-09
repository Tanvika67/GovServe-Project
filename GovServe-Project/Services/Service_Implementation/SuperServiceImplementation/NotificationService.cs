using GovServe_Project.DTOs.SupervisorDTO;
using GovServe_Project.Models.SuperModels;
using GovServe_Project.Repository.Interface;
using GovServe_Project.Repository.Interface.SuperRepositoryInterface;
using GovServe_Project.Services.Interfaces;
using GovServe_Project.Services.Interfaces.SuperServiceInterface;
using Humanizer;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
namespace GovServe_Project.Services.Service_Implementation.SuperServiceImplementation
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _repo;
        private readonly IUserRepository _userRepo;
        private readonly ICaseRepository _caseRepo;
        public NotificationService(INotificationRepository repo, IUserRepository userRepo, ICaseRepository caseRepo)
        {
            _repo = repo;
            _userRepo = userRepo;
            _caseRepo = caseRepo;
        }

			// Manual POST Notification
            public async Task SendManualNotification(NotificationDto dto)
			{
				Notification n = new Notification
                {
                    UserId = dto.UserId,
                    CaseId = dto.CaseId,
                    Message = dto.Message,
                    Category = dto.Category,
                    IsRead = false,
                    CreatedDate = DateTime.Now
                };

        await _repo.AddAsync(n);
        await _repo.SaveAsync();
    }
			// Case Created → Admin
            public async Task NotifyCaseCreated(int caseId)
			{
				var adminId = await _userRepo.GetAdminIdAsync();

             await SendNotification(
                  adminId,
					$"Case {caseId} created",
                    caseId,
					"Case Created"				
             );
}
// Case Assigned → Officer
public async Task NotifyCaseAssigned(int caseId, int officerId)
{
    await SendNotification(
        officerId,
        $"New case {caseId} assigned",
        caseId,
        "Assignment");
}
// Escalation → Officer + Citizen + Admin
public async Task NotifyCaseEscalated(int caseId, int newOfficerId)
{
    var caseData = await _caseRepo.GetByIdAsync(caseId);

    int citizenId = caseData.UserId;

    var adminId = await _userRepo.GetAdminIdAsync();

    // Officer
    await SendNotification(
    newOfficerId,
					$"Case {caseId} assigned after escalation",
					caseId,
					"Escalation"                );

    // Citizen
    await SendNotification(
    citizenId,
					$"Your case {caseId} escalated to another officer",
					caseId,
					"Escalation"                );

    // Admin
    await SendNotification(
    adminId,
					$"Case {caseId} escalated",
					caseId,
					"Escalation"                );
}
// SLA Breach → Grievance Officer
public async Task NotifySLABreach(int caseId)
{
    var grievanceId = await _userRepo.GetGrievanceOfficerIdAsync();

    await SendNotification(
        grievanceId,
        $"Case {caseId} SLA breached. Take action",
        caseId,
        "SLA");
}
// Helper Method
private async Task SendNotification(
int userId,
				string message,
				int caseId,
				string category)
			{
				Notification n = new Notification
                                 {
                                     UserId = userId,
                                     Message = message,
                                     CaseId = caseId,
                                     Category = category,
                                     IsRead = false,
                                    CreatedDate = DateTime.Now
                                 };

await _repo.AddAsync(n);
await _repo.SaveAsync();
			}
		public async Task<List<Notification>> GetUnreadNotifications(int userId)
{
    return await _repo.GetUnreadNotifications(userId);
}
public async Task<List<Notification>> GetUserNotificationsAsync(int userId)
{
    return await _repo.GetByUserIdAsync(userId);
}

public async Task<int> GetUnreadCountAsync(int userId)
{
    return await _repo.GetUnreadCountAsync(userId);
}

        public async Task MarkAsReadAsync(int notificationId)
        {
            var notification = await _repo.GetByIdAsync(notificationId);
            if (notification == null)
                throw new Exception("Notification not found");

            notification.IsRead = true;   // ✅ THIS MAKES IT READ

            _repo.Update(notification);
            await _repo.SaveAsync();
        }
    public async Task SendNotificationAsync(int userId, string message, int caseId, string category)
   {
    Notification n = new Notification();
    n.UserId = userId;
    n.CaseId = caseId; n.Message = message;
    n.Category = category;
     n.IsRead = false;
      n.CreatedDate = DateTime.Now;

    await _repo.AddAsync(n);
    await _repo.SaveAsync();
}

        public async Task SendNotificationAsync(int userId, string message, int caseId)

        {

            Notification n = new Notification
            {

                UserId = userId,

                Message = message,

                CaseId = caseId,

                Category = "Status Update", // Set a default category here     

                CreatedDate = DateTime.Now

            };


            await _repo.AddAsync(n);

            await _repo.SaveAsync();

        }

        public async Task<List<Notification>> GetReadNotifications(int userId)
        {
            return await _repo.GetReadNotifications(userId);
        }
        /* ===============================

          AUTO NOTIFY ON SLA CREATION ✅

       =============================== */

        public async Task NotifySlaCreatedAsync(int caseId)
        {
            var caseData = await _caseRepo.GetByIdAsync(caseId)
                ?? throw new Exception("Case not found");

            int officerId = caseData.AssignedOfficerId;
            if (officerId <= 0)
                throw new Exception("Assigned Officer not found");

            int supervisorId = await _userRepo.GetSupervisorIdAsync();

            await SendNotificationAsync(
                officerId,
                $"SLA has been created for Case {caseId}",
                caseId,
                "SLA"
            );

            await SendNotificationAsync(
                supervisorId,
                $"SLA has been created for Case {caseId}",
                caseId,
                "SLA"
            );
        }
    }
}
 