using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyApp.ApplicationLogic;
using MyApp.Repository;
using MyApp.Repository.ApiClient;
using WebApp;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient<IProjectsScreenUseCases, ProjectsScreenUseCases>();
builder.Services.AddTransient<IProjectRepository, ProjectRepository>();
builder.Services.AddSingleton<IWebApiExecuter>(sp => new WebApiExecuter("http://localhost:44351/", new HttpClient()));

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
