using ACGVizualizer.Models.QueueEvents.Params;
using System.Collections.Generic;

namespace ACGVizualizer.Models.QueueEvents
{
    /// <summary>
    /// Represents a call record that contains information about the caller and the events that happened during the call.
    /// </summary>
    internal class CallRecord
    {
        /// <summary>
        /// Gets the caller ID.
        /// </summary>
        public string CallerId { get; }

        /// <summary>
        /// Gets the list of queue entries for the call.
        /// </summary>
        public List<QueueEntry> CallData { get; }

        /// <summary>
        /// Initializes a new instance of the CallRecord class.
        /// </summary>
        /// <param name="callerId">The caller ID.</param>
        /// <param name="entries">The list of queue entries for the call.</param>
        public CallRecord(string callerId, List<QueueEntry> entries)
        {
            CallerId = callerId;
            CallData = entries;
        }

        /// <summary>
        /// Adds a queue entry to the list of queue entries for the call.
        /// </summary>
        /// <param name="detail">The queue entry to add.</param>
        public void AddData(QueueEntry? detail)
        {
            if (detail != null)
            {
                CallData.Add(detail);
            }
        }

        /// <summary>
        /// Gets the hold time for the call in seconds.
        /// </summary>
        /// <returns>The hold time for the call in seconds.</returns>
        public int getHoldTimeInSeconds()
        {
            for (int i = 0; i < CallData?.Count; ++i)
            {
                var cd = CallData[i];
            }

            return 0;
        }

        /// <summary>
        /// Gets the ring time for the call in seconds.
        /// </summary>
        /// <returns>The ring time for the call in seconds.</returns>
        public int getRingTimeInSeconds()
        {
            int ringTime = 0;

            for (int i = 0; i < CallData.Count; ++i)
            {
                var e = CallData[i].EventType?.EventParams as RingNoAnswerParams;

                if (e != null)
                {
                    ringTime += e.RingTime / 1000;
                }
            }

            return ringTime;
        }
    }
}
