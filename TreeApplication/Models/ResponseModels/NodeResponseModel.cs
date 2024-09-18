namespace TreeApplication.Models.ResponseModels
{
    public class NodeResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
    }
}