namespace TreeApplication.DAL.Entities
{
    public class ExceptionJournal
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string QueryParameters { get; set; }
        public string BodyParameters { get; set; }
        public string StackTrace { get; set; }
    }
}