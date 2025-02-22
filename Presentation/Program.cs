
using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Services;
using Data.Contexts;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Dialogs;
using System.Text.Encodings.Web;
using System.Text.Json;

JsonSerializerOptions options = new()
{
    WriteIndented = true,
    ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles,
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
};


var serviceCollection = new ServiceCollection()
            .AddDbContext<DataContext>(options => options.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Projects\4.DataStorage\DataStorage_Assignment\Data\Database\local_database.mdf;Integrated Security=True;Connect Timeout=30"))

            .AddScoped<IProjectService, ProjectService>()
            .AddScoped<IProjectRepository, ProjectRepository>()
            .AddScoped<ICustomerRepository, CustomerRepository>()
            .AddScoped<ICustomerService, CustomerService>()
           

            .AddScoped<IMenuDialogs, MenuDialogs>();




var serviceProvider = serviceCollection.BuildServiceProvider();

var menuDialogs = serviceProvider.GetRequiredService<IMenuDialogs>();
var projectService = serviceProvider.GetRequiredService<IProjectService>();

await menuDialogs.MenuOptions();


var result = await projectService.CreateProjectAsync(ProjectFactory.Create());

switch(result.StatusCode)
{
    case 200:
        Console.WriteLine("Project was created succesfully");
        break;

    case 400:
        Console.WriteLine($"{result.ErrorMessage}");
        break;

    case 409:
        Console.WriteLine($"{result.ErrorMessage}");
        break;

    case 500:
        Console.WriteLine($"{result.ErrorMessage}");
        break;

}
Console.ReadKey();
