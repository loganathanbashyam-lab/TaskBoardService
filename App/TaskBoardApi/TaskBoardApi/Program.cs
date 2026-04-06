using Microsoft.EntityFrameworkCore;
using TaskBoard.Data;
using TaskBoard.Data.Repositories;
using TaskBoard.Data.Repositories;
using TaskBoard.Services;
using TaskBoard.Services;


var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<TaskBoardContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddScoped<IBoardRepository, BoardRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

// Services
builder.Services.AddScoped<IBoardService, BoardService>();
builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskBoard API");
        c.RoutePrefix = string.Empty; // Serve Swagger at root in development
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();




