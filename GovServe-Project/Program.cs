
using GovServe_Project.Extensions;
using GovServe_Project.Repository.Interface;
using GovServe_Project.Repository.Repository_Implentation;
using GovServe_Project.Services.Interfaces;
using GovServe_Project.Services.Service_Implementation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

// builder.Services → dependency injection (DI) container;builder.Configuration → provides access to configuration settings (appsettings.json)
builder.Services.AddApplicationServices(builder.Configuration);

// Configure controllers and prevent JSON serializer cycles for EF navigation properties


builder.Services.AddControllers()
    .AddJsonOptions(opts =>
    {
        opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
//API documentation and testing endpoints
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

// JWT Authentication Configuration
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,

		ValidIssuer = builder.Configuration["Jwt:Issuer"],
		ValidAudience = builder.Configuration["Jwt:Audience"],

		IssuerSigningKey = new SymmetricSecurityKey(
			Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
	};
});

//builder.Services.AddCors(options =>
//{
//	options.AddPolicy("MyCorsPolicy", builder => builder
//		.WithOrigins("http://localhost:3000")
//		.AllowAnyMethod()
//		.AllowCredentials()
//		.WithHeaders("Accept", "Content-Type", "Origin", "X-My-Header"));
//});

builder.Services.AddAuthorization();


// CORS (cross-domain requests)
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAll",
		policy =>
		{
			policy.AllowAnyOrigin() 
                  .AllowAnyHeader()
				  .AllowAnyMethod();
				 
});
});

//Middleware Pipeline

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseMiddleware<ExceptionMiddleware>();
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();
app.UseCors("AllowAll");
app.UseStaticFiles();
//app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthentication();   // JWT check
app.UseAuthorization();    // Role check


app.MapControllers();

app.Run();







