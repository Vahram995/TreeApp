using TreeApplication.DAL.Entities;

namespace TreeApplication.DAL.Repositories.Abstraction
{
    public interface ITreeRepository
    {
        Task<Tree> GetTreeByNameAsync(string name);
        Task AddTreeAsync(Tree tree);
        Task SaveChangesAsync();
        Task CreateNodeAsync(Node node);
        Task<Node> GetNodeAsync(string treeName, int nodeId);
        void RemoveNode(Node node);
    }
}