namespace Team6.Models
{
    public class Session
    {
        public string SessionId { get; set; }
        public int CustomerID { get; set; }
        public DateTime Timestamp { get; set; }

        public Session()
        {
            Timestamp = DateTime.Now;
        }
    }
}
