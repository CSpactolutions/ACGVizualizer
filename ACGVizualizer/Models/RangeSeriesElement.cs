using ACGVizualizer.Models.QueueEvents;
using ACGVizualizer.Models.QueueEvents.Params;
using System.Drawing;
using System.Linq;

namespace ACGVizualizer.Models
{
    /// <summary>
    /// Represents an element in a range series plot.
    /// </summary>
    internal class RangeSeriesElement
    {
        /// <summary>
        /// The idle rectangle for the element.
        /// </summary>
        public Rectangle Idle { get; }
        /// <summary>
        /// The active rectangle for the element.
        /// </summary>
        public Rectangle Active { get; }

        /// <summary>
        /// The x-value of the element.
        /// </summary>
        public long X { get; set; }

        /// <summary>
        /// The y-value of the element.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// The scaled height of the element.
        /// </summary>
        public int Height => (int)(height * vScale);

        /// <summary>
        /// The scaled width of the element.
        /// </summary>
        public int Width => (int)(width * hScale);

        /// <summary>
        /// The horizontal scaling factor for the element.
        /// </summary>
        protected float hScale;

        /// <summary>
        /// The vertical scaling factor for the element.
        /// </summary>
        protected float vScale;

        /// <summary>
        /// The width of the element.
        /// </summary>
        protected int width;

        /// <summary>
        /// The height of the element.
        /// </summary>
        protected int height;

        /// <summary>
        /// Initializes a new instance of the RangeSeriesElement class.
        /// </summary>
        /// <param name="entry">The CallRecord entry used to create the element.</param>
        /// <param name="hScale">The horizontal scaling factor for the element.</param>
        /// <param name="vScale">The vertical scaling factor for the element.</param>
        public RangeSeriesElement(CallRecord entry, float hScale = 1, float vScale = 1)
        {
            Active = new Rectangle();
            Idle = new Rectangle();
            this.hScale = hScale;
            this.vScale = vScale;

            height = entry.CallData.Count - 2;
            width = (int)(entry.CallData[entry.CallData.Count - 1].TimeStamp - entry.CallData[0].TimeStamp);

            if (entry.CallData[entry.CallData.Count - 1].EventType?.Type == QueueEvent.EventType.ABANDON)
            {
                // ABANDON
                Idle = new Rectangle(Width, Height, Brushes.Red);
            }
            else if (entry.CallData[entry.CallData.Count - 1].EventType?.Type == QueueEvent.EventType.COMPLETEAGENT)
            {
                // COMPLETEAGENT
                var w = entry.CallData[entry.CallData.Count - 1].EventType?.EventParams as CompleteAgentParams;
                if (w != null)
                {
                    Idle = new Rectangle((int)(w.HoldTime * hScale), Height, Brushes.Yellow);
                    Active = new Rectangle((int)(w.CallTime * hScale) + 1, Height, Brushes.Green, (int)(w.HoldTime * hScale));
                }
            }
            else if (entry.CallData[entry.CallData.Count - 1].EventType?.Type == QueueEvent.EventType.COMPLETECALLER)
            {
                // COMPLETECALLER
                var w = entry.CallData[entry.CallData.Count - 1].EventType?.EventParams as CompleteCallerParams;
                if (w != null)
                {
                    Idle = new Rectangle((int)(w.HoldTime * hScale), Height, Brushes.Yellow);
                    if(w.CallTime == 0)
                    {
                        Active = new Rectangle((int)(1 * hScale), Height, Brushes.Green, (int)(w.HoldTime * hScale));
                    } else
                    {
                        Active = new Rectangle((int)(w.CallTime * hScale), Height, Brushes.Green, (int)(w.HoldTime * hScale));
                    }
                }
            }
            else
            {
                var idleWidth = (int)(entry.CallData.Last().TimeStamp - entry.CallData.First().TimeStamp);
                var idleHeight = entry.CallData.Count - 1;
                Idle = new(idleWidth, idleHeight, Brushes.Red);
            }
        }

        public bool Overlapps(RangeSeriesElement r)
        {
            return X < r.X + r.width;
        }

        /// <summary>
        /// Represents a rectangular area that can be drawn on a chart.
        /// </summary>
        internal class Rectangle
        {
            public int Width { get; }
            public int Height { get; }
            public int Offset { get; }
            public Brush Color { get; }

            /// <summary>
            /// Initializes a new instance of the Rectangle class with default values.
            /// </summary>
            public Rectangle()
            {
                Color = Brushes.Green;
            }

            /// <summary>
            /// Initializes a new instance of the Rectangle class with the specified values.
            /// </summary>
            /// <param name="width">The width of the rectangle.</param>
            /// <param name="height">The height of the rectangle.</param>
            /// <param name="color">The color of the rectangle.</param>
            /// <param name="offset">The offset of the rectangle.</param>
            public Rectangle(int width, int height, Brush color, int offset = 0)
            {
                Width = width;
                Height = height;
                Color = color;
                Offset = offset;
            }
        }
    }
}
