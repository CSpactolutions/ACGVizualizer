namespace ACGVizualizer.Models.QueueEvents.Params
{
    /// <summary>
    /// Represents the parameters for a ring no answer event.
    /// </summary>
    internal class RingNoAnswerParams : EventParams
    {
        /// <summary>
        /// Gets the amount of time the channel rang before the call was unanswered.
        /// </summary>
        public int RingTime { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RingNoAnswerParams"/> class with the specified parameters.
        /// </summary>
        /// <param name="ringtime">The amount of time the channel rang before the call was unanswered.</param>
        public RingNoAnswerParams(int ringtime)
        {
            RingTime = ringtime;
        }
    }
}
