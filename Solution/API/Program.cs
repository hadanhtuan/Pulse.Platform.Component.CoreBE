// var builder = WebApplication.CreateBuilder(args);

// /// Add services to the container.

// builder.Services.AddControllers();
// /// Learn more about configuring Swagger/OpenAPI at https:///aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen(options => {
// });
// var app = builder.Build();

// /// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// app.UseHttpsRedirection();

// app.UseAuthorization();

// app.MapControllers();

// app.Run();

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace API;
internal static class Program
{
    #if DEBUG
    global::Host.Shared.Development.DebugEnvironment.Setup();
    #endif
}