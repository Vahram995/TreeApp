namespace TreeApplication.Models.ResponseModels
{
    public class TreeResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<NodeResponseModel> Children { get; set; }
    }
}