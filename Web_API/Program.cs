using Web_API.context;
using Web_API.Contract;
using Web_API.Entities;
using Web_API.Repository;
using System.Web.Http.Cors;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using FluentAssertions.Common;
//using Microsoft.Extensions.DependencyInjection;

namespace Web_API
{
    public class Program
    {
        public static void Main(string[] args)
        {

            try
            {
                var builder = WebApplication.CreateBuilder(args);


                // Add services to the container.

                builder.Services.AddSingleton<EmployeeContext>();
                builder.Services.AddScoped<IEmpRepository, EmployeeRepository>();

                builder.Services.AddControllers();
                

                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                //builder.Services.AddSwaggerGen();
                builder.Services.AddSwaggerGen(options =>
                 {
                     options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                 });




                builder.Services.AddCors(options =>
                  options.AddPolicy("MyPolicy",
                                builder =>
                                {
                                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                                }
                     )
                );

                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }


                app.UseCors("MyPolicy");

                app.UseAuthorization();


                app.MapControllers();

                app.Run();
            }
            catch(Exception )
            {

            }
            
            
        }
    }
}