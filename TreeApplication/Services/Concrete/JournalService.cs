using TreeApplication.DAL.Repositories.Abstraction;
using TreeApplication.Models.RequestModels;
using TreeApplication.Models.ResponseModels;
using TreeApplication.Services.Abstraction;

namespace TreeApplication.Services.Concrete
{
    public class JournalService : IJournalService
    {
        private readonly IExceptionJournalRepository _journalRepository;

        public JournalService(IExceptionJournalRepository journalRepository)
        {
            _journalRepository = journalRepository;
        }

        public async Task<JournalResponseModel> GetJournalAsync(Guid id)
        {
            var journal = await _journalRepository.GetJournalAsync(id);

            return new JournalResponseModel
            {
                EventId = journal.Id,
                CreatedAt = journal.Date
            };
        }

        public async Task<JournalRangeResponseModel> GetJournalsAsync(JournalRequestModel model)
        {
            var journals = await _journalRepository.GetJournalsAsync(model.From, model.To, model.Search);

            return new JournalRangeResponseModel
            {
                Skip = model.From,
                To = model.To,
                Items = journals.Select(x => new JournalResponseModel
                {
                    EventId = x.Id,
                    CreatedAt = x.Date
                }).ToList()
            };
        }
    }
}
