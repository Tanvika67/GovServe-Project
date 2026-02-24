namespace GovServe_Project.Enum
{
	// Enum to track the status of a grievance
	public enum GrievanceStatus
	{
		Open,                  // Citizen just raised grievance
		Resolved,              // Officer or Supervisor resolved grievance
		ForwardedToSupervisor, // Grievance forwarded to Supervisor for decision
		Rejected,              // Supervisor rejected grievance → Citizen can file appeal
		Reopened               // Grievance reopened after appeal approval
	}
}

