using ACGVizualizer.Models.QueueEvents;
using System;
using System.Collections.Generic;
using static ACGVizualizer.Models.QueueEvents.QueueEvent;

namespace ACGVizualizer.Services.Parser
{
    /// <summary>
    /// Parses queue log files and returns a list of <see cref="QueueEntry"/> objects.
    /// </summary>
    internal class QueueLogParser
    {
        /// <summary>
        /// Parses an array of log file lines and returns a list of <see cref="QueueEntry"/> objects.
        /// </summary>
        /// <param name="lines">An array of log file lines.</param>
        /// <returns>A list of <see cref="QueueEntry"/> objects.</returns>
        public List<QueueEntry> ParseLog(string[] lines)
        {
            List<QueueEntry> entries = new List<QueueEntry>();

            foreach (string line in lines)
            {
                string[] fields = line.Split('|');


                QueueEntry entry = new QueueEntry(long.Parse(fields[0]), fields[1], fields[2], fields[3]);

                switch (fields[4])
                {
                    case "QUEUESTART":
                        entry.EventType = new QueueEvent(EventType.QUEUESTART, fields[5..]);
                        break;
                    case "CONFIGRELOAD":
                        entry.EventType = new QueueEvent(EventType.CONFIGRELOAD, fields[5..]);
                        break;
                    case "ENTERQUEUE":
                        entry.EventType = new QueueEvent(EventType.ENTERQUEUE, fields[5..]);
                        break;
                    case "RINGNOANSWER":
                        entry.EventType = new QueueEvent(EventType.RINGNOANSWER, fields[5..]);
                        break;
                    case "CONNECT":
                        entry.EventType = new QueueEvent(EventType.CONNECT, fields[5..]);
                        break;
                    case "COMPLETEAGENT":
                        entry.EventType = new QueueEvent(EventType.COMPLETEAGENT, fields[5..]);
                        break;
                    case "COMPLETECALLER":
                        entry.EventType = new QueueEvent(EventType.COMPLETECALLER, fields[5..]);
                        break;
                    case "ABANDON":
                        entry.EventType = new QueueEvent(EventType.ABANDON, fields[5..]);
                        break;
                    default: throw new ArgumentException("parsing error");
                }

                entries.Add(entry);
            }
            return entries;
        }
    }
}
