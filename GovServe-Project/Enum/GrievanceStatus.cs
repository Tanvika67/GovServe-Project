namespace GovServe_Project.Enum
{
	public enum GrievanceStatus
	{
		Open = 1,                 // Citizen raised grievance
		Resolved = 2,             // Issue solved by officer/supervisor
		ForwardedToSupervisor = 3,// Officer unable to solve
		Rejected = 4              // Officer/Supervisor rejected grievance
	}
}

