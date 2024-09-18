using Microsoft.EntityFrameworkCore;
using TreeApplication.DAL.Entities;
using TreeApplication.DAL.Repositories.Abstraction;

namespace TreeApplication.DAL.Repositories.Concrete
{
    public class TreeRepository : ITreeRepository
    {
        private readonly AppContext _context;

        public TreeRepository(AppContext context)
        {
            _context = context;
        }

        public async Task AddTreeAsync(Tree tree)
        {
            await _context.Trees.AddAsync(tree);
        }

        public async Task CreateNodeAsync(Node node)
        {
            await _context.Nodes.AddAsync(node);
        }

        public async Task<Node> GetNodeAsync(string treeName, int nodeId)
        {
            return await _context.Nodes
                                 .Include(n => n.Tree)
                                 .FirstOrDefaultAsync(n => n.Tree.Name == treeName && n.Id == nodeId);
        }

        public async Task<Tree> GetTreeByNameAsync(string name)
        {
            return await _context.Trees
                                    .Include(t => t.Nodes)
                                        .ThenInclude(n => n.Children)
                                    .FirstOrDefaultAsync(x => x.Name == name);
        }

        public void RemoveNode(Node node)
        {
            _context.Nodes.Remove(node);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}