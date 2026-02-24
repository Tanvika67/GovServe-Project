namespace GovServe_Project.Enum
{
	// Enum to track the status of an appeal
	public enum AppealStatus
	{
		Pending,   // Citizen filed appeal, waiting for review
		Approved,  // Appeal approved by Supervisor/Admin
		Rejected   // Appeal rejected → Final closure
	}
}
