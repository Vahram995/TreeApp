using Microsoft.AspNetCore.Mvc;
using TreeApplication.Models.RequestModels;
using TreeApplication.Models.ResponseModels;
using TreeApplication.Services.Abstraction;

namespace TreeApplication.Controllers
{
    [ApiController]
    public class ActionController : ControllerBase
    {
        private readonly ITreeService _treeService;
        private readonly IJournalService _journalService;

        public ActionController(ITreeService treeService, IJournalService journalService)
        {
            _treeService = treeService;
            _journalService = journalService;
        }

        [HttpPost("api.user.tree.get")]
        public async Task<ActionResult<TreeResponseModel>> GetTree(string name)
        {
            var tree = await _treeService.GetOrCreateTreeAsync(name);
            return Ok(tree);
        }

        [HttpPost("api.user.journal.getRange")]
        public async Task<ActionResult<JournalRangeResponseModel>> GetJournals(JournalRequestModel model)
        {
            var journals = await _journalService.GetJournalsAsync(model);
            return Ok(journals);
        }

        [HttpPost("api.user.journal.getSingle")]
        public async Task<ActionResult<JournalResponseModel>> GetJournal(Guid id)
        {
            var journal = await _journalService.GetJournalAsync(id);
            return Ok(journal);
        }

        [HttpPost("api.user.tree.node.create")]
        public async Task<ActionResult> CreateNode(CreateNodeRequestModel model)
        {
            await _treeService.CreateNodeAsync(model);
            return Ok();
        }

        [HttpPost("api.user.tree.node.delete")]
        public async Task<ActionResult> RemoveNode(string treeName, int nodeId)
        {
            await _treeService.RemoveNodeAsync(treeName, nodeId);
            return Ok();
        }

        [HttpPost("api.user.tree.node.rename")]
        public async Task<ActionResult> UpdateNode(UpdateNodeRequestModel model)
        {
            await _treeService.UpdateNodeAsync(model);
            return Ok();
        }
    }
}