namespace DataHandler.Entities
{
    public class CallDetailRecord
    {
        ///// <summary>
        ///// Call Detail Record PrimaryID
        ///// </summary>
        //public Guid Id { get; set; }
        /// <summary>
        /// Unique reference for the call
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// Phone number of the caller
        /// </summary>
        public string CallerId { get; set; }
        /// <summary>
        /// Phone number of the number dialled
        /// </summary>
        public string Recipient { get; set; }
        /// <summary>
        /// Date on which the call was made
        /// </summary>
        public DateTime CallDate { get; set; }
        /// <summary>
        /// Time when the call ended
        /// </summary>
        public TimeSpan EndTime { get; set; }
        /// <summary>
        /// Duration of the call in Seconds
        /// </summary>
        public int Duration { get; set; }
        /// <summary>
        /// The billable cost of the call, To 3 decimal places (decipence)
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// Currency for the cost (alpha-3 ISO code)
        /// </summary>
        public string Currency { get; set; }
    }
}