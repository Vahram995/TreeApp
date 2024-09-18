namespace TreeApplication.Models.RequestModels
{
    public class UpdateNodeRequestModel
    {
        public string TreeName { get; set; }
        public int NodeId { get; set; }
        public string NewNodeName { get; set; }
    }
}
