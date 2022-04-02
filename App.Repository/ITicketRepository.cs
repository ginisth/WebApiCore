using Core.Models;

namespace App.Repository
{
    public interface ITicketRepository
    {
        Task<int> CreateAsync(Ticket ticket);
        Task DeleteAsync(int id);
        Task<IEnumerable<Ticket>> GetAsync(string filter = null);
        Task<Ticket> GetByIdAsync(int id);
        Task UpdateAsync(Ticket ticket);
    }
}