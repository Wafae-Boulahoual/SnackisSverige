using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace SnackisForum.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var builder = WebApplication.CreateBuilder(args);

            //builder.Services.AddControllers();
            //builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();

            //// DbContext — samma connection string som Presentation
            //builder.Services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnectionString")));

            //// Repositories
            //builder.Services.AddScoped<IPostRepository, PostRepository>();
            //builder.Services.AddScoped<ICommentRepository, CommentRepository>();

            //// Services
            //builder.Services.AddScoped<IPostServices, PostServices>();
            //builder.Services.AddScoped<ICommentServices, CommentServices>();

            //var app = builder.Build();

            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}

            //app.UseHttpsRedirection();
            //app.UseAuthorization();
            //app.MapControllers();
            //app.Run();
        }
    }
}