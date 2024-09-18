namespace TreeApplication.DAL.Entities
{
    public class Node
    {
        public int Id { get; set; }
        public int TreeId { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }

        public Tree Tree { get; set; }
        public Node Parent { get; set; }
        public ICollection<Node> Children { get; set; }
    }
}
