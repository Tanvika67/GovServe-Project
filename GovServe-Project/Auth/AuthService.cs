using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GovServe_Project.Data;
using GovServe_Project.DTOs;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;

namespace GovServe_Project.Auth
{
    public class AuthService:IAuthService
    {
		private readonly GovServe_ProjectContext _context;
		private readonly IConfiguration _config;

		public AuthService(GovServe_ProjectContext context, IConfiguration config)
		{
			_context = context;
			_config = config;
		}

		public async Task<string> Login(LoginDTO dto)
		{
			// Check user
			var user = await _context.User
				.FirstOrDefaultAsync(x => x.Email == dto.Email);

			if (user == null)
				throw new Exception("User not found");

			// Password check (plain for now)
			if (user.Password != dto.Password)
				throw new Exception("Invalid Password");

			// Claims
			var claims = new[]
			{
			new Claim(ClaimTypes.Name, user.Email),
			new Claim(ClaimTypes.Role, user.RoleName.ToString()),
			new Claim("UserId", user.UserId.ToString())
		};

			// Key
			var key = new SymmetricSecurityKey(
				Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

			var creds = new SigningCredentials(
				key, SecurityAlgorithms.HmacSha256);

			// Token
			var token = new JwtSecurityToken(
				issuer: _config["Jwt:Issuer"],
				audience: _config["Jwt:Audience"],
				claims: claims,
				expires: DateTime.Now.AddHours(2),
				signingCredentials: creds
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
