using Core.Models;

namespace MyApp.Repository
{
    public interface IProjectRepository
    {
        Task<int> CreateAsync(Project project);
        Task DeleteAsync(int id);
        Task<IEnumerable<Project>> GetAsync();
        Task<Project> GetByIdAsync(int id);
        Task<IEnumerable<Ticket>> GetProjectTicketsAsync(int projectId);
        Task UpdateAsync(Project project);
    }
}