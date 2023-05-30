using Microsoft.EntityFrameworkCore;
using TodoAPI.Models;
using System.Linq;


namespace TodoAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase("TodoList"));

            builder.Services.AddControllers();
        
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<TodoContext>();
    
    // Update the data items here
    var todoItems = dbContext.TodoItems.ToList();
    // Modify the todoItems as required
    // For example, to add a new item:
    todoItems.Add(new TodoItem { Name = "quiz#3" });

    // Save the changes
    dbContext.SaveChanges();
}


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
