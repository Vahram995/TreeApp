using Microsoft.EntityFrameworkCore;
using TreeApplication.DAL.Entities;
using TreeApplication.DAL.Repositories.Abstraction;
using TreeApplication.Exceptions;
using TreeApplication.Models.RequestModels;
using TreeApplication.Models.ResponseModels;
using TreeApplication.Services.Abstraction;
using static NpgsqlTypes.NpgsqlTsQuery;

namespace TreeApplication.Services.Concrete
{
    public class TreeService : ITreeService
    {
        private readonly ITreeRepository _treeRepository;

        public TreeService(ITreeRepository treeRepository)
        {
            _treeRepository = treeRepository;
        }

        public async Task CreateNodeAsync(CreateNodeRequestModel model)
        {
            var tree = await _treeRepository.GetTreeByNameAsync(model.TreeName);

            if (tree == null)
            {
                throw new SecureException($"{model.TreeName} tree was not found.");
            }

            await _treeRepository.CreateNodeAsync(new Node
            {
                Name = model.NodeName,
                TreeId = tree.Id,
                ParentId = model.ParentNodeId
            });

            await _treeRepository.SaveChangesAsync();
        }

        public async Task<TreeResponseModel> GetOrCreateTreeAsync(string name)
        {
            var tree = await _treeRepository.GetTreeByNameAsync(name);

            if (tree == null)
            {
                tree = new Tree
                {
                    Name = name,
                    Nodes = new List<Node>()
                };

                await _treeRepository.AddTreeAsync(tree);
                await _treeRepository.SaveChangesAsync();
            }

            return new TreeResponseModel
            {
                Id = tree.Id,
                Name = tree.Name,
                Children = tree.Nodes.Select(x => new NodeResponseModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    ParentId = x.ParentId
                }).ToList()
            };
        }

        public async Task RemoveNodeAsync(string treeName, int nodeId)
        {
            var node = await _treeRepository.GetNodeAsync(treeName, nodeId);

            if (node == null)
            {
                throw new SecureException($"Node was not found.");
            }

            _treeRepository.RemoveNode(node);
            await _treeRepository.SaveChangesAsync();
        }

        public async Task UpdateNodeAsync(UpdateNodeRequestModel model)
        {
            var node = await _treeRepository.GetNodeAsync(model.TreeName, model.NodeId);

            if (node == null)
            {
                throw new SecureException($"Node was not found.");
            }

            node.Name = model.NewNodeName;

            await _treeRepository.SaveChangesAsync();
        }
    }
}
