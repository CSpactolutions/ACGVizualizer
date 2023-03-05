namespace ACGVizualizer.Models.QueueEvents.Params
{
    /// <summary>
    /// Represents the parameters for a connect event.
    /// </summary>
    internal class ConnectParams : EventParams
    {
        /// <summary>
        /// Gets the total amount of time the channel was on hold, in seconds.
        /// </summary>
        public int HoldTime { get; }

        /// <summary>
        /// Gets the unique ID of the channel that was bridged with the connected channel.
        /// </summary>
        public string BridgedChannelUniqueId { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectParams"/> class with the specified parameters.
        /// </summary>
        /// <param name="holdtime">The total amount of time the channel was on hold, in seconds.</param>
        /// <param name="bcuId">The unique ID of the channel that was bridged with the connected channel.</param>
        public ConnectParams(int holdtime, string bcuId)
        {
            HoldTime = holdtime;
            BridgedChannelUniqueId = bcuId;
        }
    }
}
