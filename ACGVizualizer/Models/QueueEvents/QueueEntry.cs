namespace ACGVizualizer.Models.QueueEvents
{
    /// <summary>
    /// Represents an entry in a queue event log.
    /// </summary>
    internal class QueueEntry
    {
        /// <summary>
        /// Gets the timestamp of the queue event.
        /// </summary>
        public long TimeStamp { get; }

        /// <summary>
        /// Gets the caller ID of the queue event.
        /// </summary>
        public string CallerId { get; }

        /// <summary>
        /// Gets the extension line of the queue event.
        /// </summary>
        public string ExtentionLine { get; }

        /// <summary>
        /// Gets the channel of the queue event.
        /// </summary>
        public string Channel { get; }

        /// <summary>
        /// Gets or sets the type of the queue event.
        /// </summary>
        public QueueEvent? EventType { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueueEntry"/> class.
        /// </summary>
        /// <param name="timeStamp">The timestamp of the queue event.</param>
        /// <param name="callerId">The caller ID of the queue event.</param>
        /// <param name="extentionLine">The extension line of the queue event.</param>
        /// <param name="channel">The channel of the queue event.</param>
        public QueueEntry(long timeStamp, string callerId, string extentionLine, string channel)
        {
            TimeStamp = timeStamp;
            CallerId = callerId;
            ExtentionLine = extentionLine;
            Channel = channel;
        }
    }

}
