using TreeApplication.Models.RequestModels;
using TreeApplication.Models.ResponseModels;

namespace TreeApplication.Services.Abstraction
{
    public interface IJournalService
    {
        Task<JournalResponseModel> GetJournalAsync(Guid id);
        Task<JournalRangeResponseModel> GetJournalsAsync(JournalRequestModel model);
    }
}
