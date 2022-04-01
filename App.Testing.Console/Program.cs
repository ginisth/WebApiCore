// See https://aka.ms/new-console-template for more information

using App.Repository;
using App.Repository.ApiClient;
using Core.Models;

HttpClient httpClient = new HttpClient();
IWebApiExecuter webApiExecuter = new WebApiExecuter("https://localhost:44351", httpClient);

await TestTickets();

#region Test Projects
Console.WriteLine("//////////////////////////////////");
Console.WriteLine("Reading Projects");
await GetProjects();

Console.WriteLine("//////////////////////////////////");
Console.WriteLine("Reading Project tickets");
await GetProjectTickets(1);

Console.WriteLine("//////////////////////////////////");
Console.WriteLine("Create Project");
var pid = await CreateProject();
await GetProjects();

Console.WriteLine("//////////////////////////////////");
Console.WriteLine("Update Project");
var project = await GetProject(pid);
await UpdateProject(project);
await GetProjects();

Console.WriteLine("//////////////////////////////////");
Console.WriteLine("delete Project");
await DeleteProject(pid);
await GetProjects();

async Task GetProjects()
{
    ProjectRepository repository = new ProjectRepository(webApiExecuter);
    var projects = await repository.GetAsync();

    foreach (var project in projects)
    {
        Console.WriteLine($"Project: {project.Name}");
    }
}

async Task<Project> GetProject(int id)
{
    ProjectRepository repository = new ProjectRepository(webApiExecuter);
    return await repository.GetByIdAsync(id);
}

async Task GetProjectTickets(int id)
{
    var project = await GetProject(id);
    Console.WriteLine($"Project: {project.Name}");
    ProjectRepository repository = new ProjectRepository(webApiExecuter);
    var tickets = await repository.GetProjectTicketsAsync(id);

    foreach (var ticket in tickets)
    {
        Console.WriteLine($"Ticket: {ticket.Title}");
    }

}

async Task<int> CreateProject()
{
    var project = new Project { Name = "Another Project" };
    ProjectRepository repository = new ProjectRepository(webApiExecuter);
    return await repository.CreateAsync(project);
}

async Task UpdateProject(Project project)
{
    ProjectRepository repository = new ProjectRepository(webApiExecuter);
    project.Name += "updated";
    await repository.UpdateAsync(project);
}

async Task DeleteProject(int id)
{
    ProjectRepository repository = new ProjectRepository(webApiExecuter);
    await repository.DeleteAsync(id);
}
#endregion

#region Test Tickets
async Task TestTickets()
{
    Console.WriteLine("////////////////////");
    Console.WriteLine("Reading all tickets...");
    await GetTickets();

    Console.WriteLine("////////////////////");
    Console.WriteLine("Reading contains 1...");
    await GetTickets("1");

    Console.WriteLine("////////////////////");
    Console.WriteLine("Create a ticket...");
    var tId = await CreateTicket();
    await GetTickets();

    Console.WriteLine("////////////////////");
    Console.WriteLine("Update a ticket...");
    var ticket = await GetTicketById(tId);
    await UpdateTicket(ticket);
    await GetTickets();

    Console.WriteLine("////////////////////");
    Console.WriteLine("Delete a ticket...");
    await DeleteTicket(tId);
    await GetTickets();
}

async Task GetTickets(string filter = null)
{
    TicketRepository ticketRepository = new(webApiExecuter);
    var tickets = await ticketRepository.GetAsync(filter);
    foreach (var ticket in tickets)
    {
        Console.WriteLine($"Ticket: {ticket.Title}");
    }
}

async Task<Ticket> GetTicketById(int id)
{
    TicketRepository ticketRepository = new(webApiExecuter);
    var ticket = await ticketRepository.GetByIdAsync(id);
    return ticket;
}

async Task<int> CreateTicket()
{
    TicketRepository ticketRepository = new(webApiExecuter);
    return await ticketRepository.CreateAsync(new Ticket
    {
        ProjectId = 2,
        Title = "This a very difficult.",
        Description = "Something is wrong on the server."
    });
}

async Task UpdateTicket(Ticket ticket)
{
    TicketRepository ticketRepository = new(webApiExecuter);
    ticket.Title += " Updated";
    await ticketRepository.UpdateAsync(ticket);
}

async Task DeleteTicket(int id)
{
    TicketRepository ticketRepository = new(webApiExecuter);
    await ticketRepository.DeleteAsync(id);
}

#endregion
