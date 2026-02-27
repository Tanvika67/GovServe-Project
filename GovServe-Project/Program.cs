using GovServe_Project.Data;
using GovServe_Project.Extensions;
using GovServe_Project.Repository.Interface;
using GovServe_Project.Repository.Interface.SuperRepositoryInterface;
using GovServe_Project.Repository.Repository_Implentation;
using GovServe_Project.Repository.Repository_Implentation.SuperRepositoryImplementation;
using GovServe_Project.Services.Interfaces;
using GovServe_Project.Services.Interfaces.SuperServiceInterface;
using GovServe_Project.Services.Service_Implementation.SuperServiceImplementation;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<GovServe_ProjectContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("GovServe_ProjectContext")));


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICaseRepository, CaseRepository>();
builder.Services.AddScoped<ICaseService, CaseService>();

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

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//app.UseRouting();
app.UseMiddleware<ExceptionMiddleware>();
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();


app.Run();





