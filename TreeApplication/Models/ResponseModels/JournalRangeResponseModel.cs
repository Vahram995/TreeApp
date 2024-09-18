namespace TreeApplication.Models.ResponseModels
{
    public class JournalRangeResponseModel
    {
        public int Skip { get; set; }
        public int To { get; set; }
        public List<JournalResponseModel> Items { get; set; }
    }
}
