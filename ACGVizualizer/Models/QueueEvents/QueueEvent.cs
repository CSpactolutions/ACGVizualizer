using ACGVizualizer.Models.QueueEvents.Params;

namespace ACGVizualizer.Models.QueueEvents
{
    /// <summary>
    /// Represents an event that occurred in the queue system.
    /// </summary>
    internal class QueueEvent
    {
        /// <summary>
        /// The type of the event.
        /// </summary>
        public EventType Type { get; set; }

        /// <summary>
        /// The parameters associated with the event, if any.
        /// </summary>
        public EventParams? EventParams { get; set; }

        /// <summary>
        /// Enumerates the possible event types.
        /// </summary>
        public enum EventType
        {
            QUEUESTART,
            CONFIGRELOAD,
            ENTERQUEUE,
            RINGNOANSWER,
            CONNECT,
            COMPLETEAGENT,
            COMPLETECALLER,
            ABANDON
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueueEvent"/> class with the specified type and parameters.
        /// </summary>
        /// <param name="type">The type of the event.</param>
        /// <param name="parameters">The parameters associated with the event, if any.</param>
        public QueueEvent(EventType type, string[] parameters)
        {
            Type = type;
            switch (type)
            {
                case EventType.ENTERQUEUE:
                    EventParams = new EnterQueueParams(parameters[0], parameters[1]);
                    break;
                case EventType.RINGNOANSWER:
                    EventParams = new RingNoAnswerParams(int.Parse(parameters[0]));
                    break;
                case EventType.CONNECT:
                    EventParams = new ConnectParams(int.Parse(parameters[0]), parameters[1]);
                    break;
                case EventType.COMPLETEAGENT:
                    if (parameters.Length == 2)
                    {
                        EventParams = new CompleteAgentParams(int.Parse(parameters[0]), int.Parse(parameters[1]), null);
                    }
                    else
                    {
                        EventParams = new CompleteAgentParams(int.Parse(parameters[0]), int.Parse(parameters[1]), int.Parse(parameters[2]));
                    }
                    break;
                case EventType.COMPLETECALLER:
                    if (parameters.Length == 2)
                    {
                        EventParams = new CompleteCallerParams(int.Parse(parameters[0]), int.Parse(parameters[1]), null);
                    }
                    else
                    {
                        EventParams = new CompleteCallerParams(int.Parse(parameters[0]), int.Parse(parameters[1]), int.Parse(parameters[2]));
                    }
                    break;
                case EventType.ABANDON:
                    EventParams = new AbandonParams(int.Parse(parameters[0]), int.Parse(parameters[1]), int.Parse(parameters[2]));
                    break;
                default:
                    EventParams = null;
                    break;
            }
        }
    }
}
