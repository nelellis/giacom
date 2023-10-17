namespace DataHandler.ViewModels
{
    public class CallDetailRecordDto
    {
        public string Reference { get; set; }
        public string CallerId { get; set; }
        public string Recipient { get; set; }
        public DateTime CallDate { get; set; }
        public TimeSpan EndTime { get; set; }
        public int Duration { get; set; }
        public decimal Cost { get; set; }
        public string Currency { get; set; }
    }
}