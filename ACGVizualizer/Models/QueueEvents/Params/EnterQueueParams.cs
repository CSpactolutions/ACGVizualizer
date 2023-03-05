namespace ACGVizualizer.Models.QueueEvents.Params
{
    /// <summary>
    /// Represents the parameters for an enter queue event.
    /// </summary>
    internal class EnterQueueParams : EventParams
    {
        /// <summary>
        /// Gets the URL of the channel that entered the queue.
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// Gets the caller ID of the channel that entered the queue.
        /// </summary>
        public string CallerId { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnterQueueParams"/> class with the specified parameters.
        /// </summary>
        /// <param name="url">The URL of the channel that entered the queue.</param>
        /// <param name="callerId">The caller ID of the channel that entered the queue.</param>
        public EnterQueueParams(string url, string callerId)
        {
            Url = url;
            CallerId = callerId;
        }
    }
}
