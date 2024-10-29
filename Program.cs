using ExpenseTrackerAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ExpenseTrackerAPI.Core;
using ExpenseTrackerAPI.Core.Profiles;
using ExpenseTrackerAPI.Interfaces;
using ExpenseTrackerAPI.Interfaces.Repositories;
using ExpenseTrackerAPI.Interfaces.Services;
using ExpenseTrackerAPI.Repositories;
using ExpenseTrackerAPI.Services;
using ExpenseTrackerAPI.Services.interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers(options => { options.Filters.Add<GlobalExceptionFilter>(); });

// Dependency injection
builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

// Add database context
builder.Services.AddDbContext<ExpenseTrackerContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policyBuilder =>
        {
            policyBuilder // or the actual URL of your React app
                .WithOrigins("http://localhost:3000") // Specify your allowed domain here
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

// Auto-mapping
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Configure JWT authentication
// var key = Encoding.ASCII.GetBytes("DGYecfhL/yMddqEecxu66h702G7iPZQ0WPPSjI+Umas=");
var key = Encoding.ASCII.GetBytes("DGYecfhL/yMddqEecxu66h702G7iPZQ0WPPSjI+Umas=12345678901234567890123456789012");

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });


// Add Controllers
builder.Services.AddControllers();

// token
builder.Services.AddSingleton<ITokenBlacklistService, TokenBlacklistService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors("AllowAllOrigins");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<TokenBlacklistMiddleware>();

app.Run();