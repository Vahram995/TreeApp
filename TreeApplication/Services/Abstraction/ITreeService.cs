using TreeApplication.Models.RequestModels;
using TreeApplication.Models.ResponseModels;

namespace TreeApplication.Services.Abstraction
{
    public interface ITreeService
    {
        Task CreateNodeAsync(CreateNodeRequestModel model);
        Task<TreeResponseModel> GetOrCreateTreeAsync(string name);
        Task RemoveNodeAsync(string treeName, int nodeId);
        Task UpdateNodeAsync(UpdateNodeRequestModel model);
    }
}