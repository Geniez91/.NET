using Microsoft.EntityFrameworkCore;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=user.db"));

builder.Services.AddScoped<ArticleService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddControllers();


var app = builder.Build();
app.MapControllers();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.Run();
