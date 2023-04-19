namespace Team6.Models
{
    public class Session
    {
        public string SessionID { get; set; }
        public int CustomerID { get; set; }
        public DateTime Timestamp { get; set; }

        public Session()
        {
            Timestamp = DateTime.Now;
        }
    }

    public class SessionResult
    {
        public string SessionID { get; set; }
        public string CustomerID { get; set; }
        public DateTime Timestamp { get; set; }

        public SessionResult()
        {
            Timestamp = DateTime.Now;
        }
    }
}
