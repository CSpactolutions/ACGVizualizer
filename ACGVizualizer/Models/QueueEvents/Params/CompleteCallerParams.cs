namespace ACGVizualizer.Models.QueueEvents.Params
{
    /// <summary>
    /// Represents the parameters for a complete caller event.
    /// </summary>
    internal class CompleteCallerParams : EventParams
    {
        /// <summary>
        /// Gets the total amount of time the call was on hold, in seconds.
        /// </summary>
        public int HoldTime { get; }

        /// <summary>
        /// Gets the total amount of time the caller spent on the call, in seconds.
        /// </summary>
        public int CallTime { get; }

        /// <summary>
        /// Gets the original position of the call in the queue before any queue jumps.
        /// </summary>
        public int? OriginalPosition { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompleteCallerParams"/> class with the specified parameters.
        /// </summary>
        /// <param name="holdtime">The total amount of time the call was on hold, in seconds.</param>
        /// <param name="calltime">The total amount of time the caller spent on the call, in seconds.</param>
        /// <param name="origpos">The original position of the call in the queue before any queue jumps, or null if the position is unknown.</param>
        public CompleteCallerParams(int holdtime, int calltime, int? origpos)
        {
            HoldTime = holdtime;
            CallTime = calltime;
            OriginalPosition = origpos;
        }
    }
}
