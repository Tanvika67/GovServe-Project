namespace GovServe_Project.Extensions
{
	public static class FileValidationExtension
	{
		// Validate file extension and size
		public static bool IsValidDocument(this IFormFile file)
		{
			if (file == null)
				return false;

			// Allowed extensions
			var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".pdf" };

			var extension = Path.GetExtension(file.FileName).ToLower();

			// Extension check
			if (!allowedExtensions.Contains(extension))
				return false;

			// Size check (2MB)
			if (file.Length > 2 * 1024 * 1024)
				return false;

			return true;
		}
	}
}
