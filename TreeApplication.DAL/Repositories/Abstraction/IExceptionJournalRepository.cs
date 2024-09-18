using TreeApplication.DAL.Entities;

namespace TreeApplication.DAL.Repositories.Abstraction
{
    public interface IExceptionJournalRepository
    {
        Task AddAsync(ExceptionJournal entity);
        Task<ExceptionJournal> GetJournalAsync(Guid id);
        Task<List<ExceptionJournal>> GetJournalsAsync(int from, int to, string search);
        Task SaveChangesAsync();
    }
}
