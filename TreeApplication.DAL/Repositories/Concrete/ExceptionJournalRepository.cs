using Microsoft.EntityFrameworkCore;
using TreeApplication.DAL.Entities;
using TreeApplication.DAL.Repositories.Abstraction;

namespace TreeApplication.DAL.Repositories.Concrete
{
    public class ExceptionJournalRepository : IExceptionJournalRepository
    {
        private readonly AppContext _context;

        public ExceptionJournalRepository(AppContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ExceptionJournal entity)
        {
           await _context.AddAsync(entity);
        }

        public async Task<List<ExceptionJournal>> GetJournalsAsync(int from, int to, string search)
        {
            var count = to - from > 0 ? to - from : 0;

            var query = _context.ExceptionJournals
                                    .OrderBy(x => x.Date)
                                    .Skip(from)
                                    .Take(count)
                                    .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x => x.QueryParameters.Contains(search) || x.BodyParameters.Contains(search)); 
            }

            return await query.ToListAsync();
        }

        public async Task<ExceptionJournal> GetJournalAsync(Guid id)
        {
            return await _context.ExceptionJournals.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}