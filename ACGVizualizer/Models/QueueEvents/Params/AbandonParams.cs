namespace ACGVizualizer.Models.QueueEvents.Params
{
    /// <summary>
    /// Represents the parameters for an abandon event.
    /// </summary>
    internal class AbandonParams : EventParams
    {
        /// <summary>
        /// Gets the position of the abandoned call in the queue.
        /// </summary>
        public int Position { get; }

        /// <summary>
        /// Gets the original position of the call in the queue before any queue jumps.
        /// </summary>
        public int OriginalPosition { get; }

        /// <summary>
        /// Gets the amount of time the call waited in the queue before being abandoned, in seconds.
        /// </summary>
        public int WaitTime { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbandonParams"/> class with the specified parameters.
        /// </summary>
        /// <param name="pos">The position of the abandoned call in the queue.</param>
        /// <param name="origpos">The original position of the call in the queue before any queue jumps.</param>
        /// <param name="waittime">The amount of time the call waited in the queue before being abandoned, in seconds.</param>
        public AbandonParams(int pos, int origpos, int waittime)
        {
            Position = pos;
            OriginalPosition = origpos;
            WaitTime = waittime;
        }
    }
}
