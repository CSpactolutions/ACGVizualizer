using System.Collections.Generic;

namespace ACGVizualizer.Models.QueueEvents
{
    /// <summary>
    /// Represents a collection of call records.
    /// </summary>
    internal class CallRecordCollection
    {
        /// <summary>
        /// Gets or sets the list of call records.
        /// </summary>
        public List<CallRecord> CallRecords;

        /// <summary>
        /// Initializes a new instance of the <see cref="CallRecordCollection"/> class with the specified queue entry list.
        /// </summary>
        /// <param name="entriesList">The list of queue entries.</param>
        public CallRecordCollection(List<QueueEntry> entriesList)
        {
            CallRecords = new List<CallRecord>();
            Initialize(entriesList);
        }

        /// <summary>
        /// Initializes the call records using the specified queue entry list.
        /// </summary>
        /// <param name="entriesList">The list of queue entries.</param>
        protected void Initialize(List<QueueEntry> entriesList)
        {
            var entryMap = new Dictionary<string, List<QueueEntry>>();
            List<QueueEntry>? entries;

            foreach (var entry in entriesList)
            {

                if (!entryMap.TryGetValue(entry.CallerId, out entries))
                {
                    entries = new List<QueueEntry>();
                    entryMap[entry.CallerId] = entries;
                }

                entries.Add(entry);
            }
            foreach (var key in entryMap.Keys)
            {
                if (!key.Equals("NONE"))
                {
                    var record = new CallRecord(key, entryMap[key]);
                    CallRecords?.Add(record);

                }
            }
        }
    }
}
