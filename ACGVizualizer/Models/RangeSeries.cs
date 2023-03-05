using ACGVizualizer.Models.QueueEvents;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace ACGVizualizer.Models
{
    /// <summary>
    /// Represents a series of elements, each of which represents a call queue or agent range.
    /// </summary>
    internal class RangeSeries
    {
        private const int MinHeight = 200;
        private const int MinWidth = 400;

        private const int padding = 40;

        // The Width and Height of the scaled bitmap
        private int bitmapWidth = 0;
        private int bitmapHeight = 0;

        // Total log time
        private int TimeRange = 0;
        private long StartTime = 0;

        protected long startX;

        // HScale and VScale of data units on Axis. 
        public float HScale { get; set; } = 3;
        public float VScale { get; set; } = 4;

        // Defines the number of units to display on the x-axis.
        public int XUnitsCount { get; set; }
        public int YUnitsCount { get; set; }


        /// <summary>
        /// The collection of elements in the range series.
        /// </summary>
        public List<RangeSeriesElement> Elements;

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeSeries"/> class.
        /// </summary>
        /// <param name="collection">The collection of call records.</param>
        public RangeSeries(CallRecordCollection collection)
        {
            RangeSeriesElement? previousElement = null;

            Elements = new List<RangeSeriesElement>();

            // Calculate the start and end times of the call records
            var last = collection.CallRecords.Last().CallData.Last().TimeStamp;
            var first = collection.CallRecords.First().CallData.First().TimeStamp;
            startX = first - padding;
            TimeRange = (int)(last - first);
            StartTime = first;

            bitmapWidth = (int)((last - first) * HScale) + 2 * padding;

            // Create a range series element for each call record and calculate its position in the series
            foreach (var entry in collection.CallRecords)
            {
                var element = new RangeSeriesElement(entry, HScale, VScale);
                
                element.X = entry.CallData[0].TimeStamp;

                element.Y = previousElement == null
                    ? 0 : element.Overlapps(previousElement)
                    ? previousElement.Y + previousElement.Height : 0;

                previousElement = element;
                Elements.Add(element);

                bitmapHeight = Math.Max(bitmapHeight, element.Y + element.Height);
            }

            XUnitsCount = (int)(TimeRange / 60);
            YUnitsCount = (int)((bitmapHeight / VScale) + VScale);
        }

        /// <summary>
        /// Exports the range series to a bitmap file.
        /// </summary>
        /// <param name="filePath">The path of the file to export to.</param>
        /// <param name="width">The width of the exported bitmap.</param>
        /// <param name="height">The height of the exported bitmap.</param>
        /// <returns>A bitmap image of the range series.</returns>
        public Bitmap ExportToBitmap(int width, int height)
        {
            // Create a new bitmap of width x height 

            var bitmap = ExportToBitmap();

            float hScalingFactor = (float)width / bitmap.Width;
            float vScalingFactor = (float)height / bitmap.Height;
            int newWidth = (int)(bitmap.Width * hScalingFactor);
            int newHeight = (int)(bitmap.Height * vScalingFactor);

            Bitmap scaledImage = new Bitmap(newWidth, newHeight);

            Graphics g = Graphics.FromImage(scaledImage);

            g.CompositingQuality = CompositingQuality.HighQuality;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = PixelOffsetMode.HighSpeed;
            g.SmoothingMode = SmoothingMode.None;

            g.DrawImage(bitmap, new Rectangle(0, 0, newWidth, newHeight), new Rectangle(0, 0, bitmap.Width, bitmap.Height), GraphicsUnit.Pixel);

            return scaledImage;
        }

        /// <summary>
        /// Exports the range series to a bitmap file.
        /// </summary>
        /// <param name="filePath">The path of the file to export to.</param>
        /// <returns>A bitmap image of the range series.</returns>
        public Bitmap ExportToBitmap()
        {
            // Create a new bitmap of width x height 
            var bitmap = new Bitmap(Math.Max(MinWidth, bitmapWidth + 2 * padding), Math.Max(MinHeight, bitmapHeight + 2 * padding));

            //Create a new graphics object that allows us to draw on the bitmap
            var graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);

            //  use the graphics object to draw to the bitmap
            foreach (var element in Elements)
            {
                DrawElement(graphics, element, bitmap.Height);
            }

            DrawAxes(graphics, bitmap.Width, bitmap.Height);

            return bitmap;
        }

        /// <summary>
        /// Draws a range series element on the chart.
        /// </summary>
        /// <param name="graphics">The graphics object to use for drawing.</param>
        /// <param name="element">The range series element to draw.</param>
        /// <param name="height">The height of the chart area.</param>
        private void DrawElement(Graphics graphics, RangeSeriesElement element, int height)
        {
            // Calculate the positions of the idle and active parts of the element.
            var idleX = (int)((element.X - startX) * HScale + 1);
            var idleY = height - element.Y - element.Idle.Height - 1;
            var activeX = idleX + element.Active.Offset;
            var activeY = height - element.Y - element.Active.Height - 1;

            // Draw the idle part of the element.
            graphics.FillRectangle(element.Idle.Color, new Rectangle(idleX, idleY - padding + 1, element.Idle.Width, element.Idle.Height));
            graphics.DrawRectangle(new Pen(Color.Black), new Rectangle(idleX, idleY - padding + 1, element.Idle.Width, element.Idle.Height));

            // Draw a vertical line to indicate the element value on the x-axis.
            //graphics.DrawLine(new Pen(Color.Black), idleX, height - padding + 5, idleX, height - padding); // element value on x axis

            // Draw the active part of the element.
            graphics.FillRectangle(element.Active.Color, new Rectangle(activeX, activeY - padding + 1, element.Active.Width, element.Active.Height));
            graphics.DrawRectangle(new Pen(Color.Black, 1), new Rectangle(activeX, activeY - padding + 1, element.Active.Width, element.Active.Height));
        }

        /// <summary>
        /// Draws the x- and y-axes of the chart.
        /// </summary>
        /// <param name="graphics">The graphics object to use for drawing.</param>
        /// <param name="width">The width of the chart area.</param>
        /// <param name="height">The height of the chart area.</param>
        private void DrawAxes(Graphics graphics, int width, int height)
        {
            // Draw the x- and y-axes.
            Pen axisPen = new Pen(Color.Black);
            graphics.DrawLine(axisPen, padding, height - padding, width - padding, height - padding); // x-axis
            graphics.DrawLine(axisPen, padding, padding - 20, padding, height - padding); ; // y-axis

            // Add axis labels.
            Font font = new Font("Arial", 8);
            graphics.DrawString("Timestamp", font, Brushes.Black, width - 70, height - 30);
            graphics.DrawString("Number of Agents", font, Brushes.Black, 2, 5);

            // Add y-axis tick marks and labels.
            font = new Font("Arial", 6);

            var yUnitGap = (bitmapHeight + padding) / YUnitsCount;

            for (int i = 0; i <= YUnitsCount; ++i)
            {
                var yCoord = i * yUnitGap;
                // For every 5 units, draw a line on the y-axis and draw the string value.
                if (i % 5 == 0)
                {
                    graphics.DrawLine(new Pen(Color.Black), padding - 2, height - yCoord - padding, padding + 2, height - yCoord - padding);
                    graphics.DrawString((i).ToString(), font, Brushes.Black, new Point(padding - (int)(5 * HScale) - 10, height - (int)yCoord - padding));
                }
            }

            // Add x-axis tick marks and labels.
            var xUnitGap = TimeRange * HScale / XUnitsCount;
            var t = StartTime;

            for (int i = 0; i <= XUnitsCount; ++i)
            {
                var xCoord = i * xUnitGap + padding;

                graphics.DrawLine(new Pen(Color.Black), xCoord, height - padding - 2, xCoord, height - padding + 2);

                // Convert to unix timestamp
                DateTime unixTime = DateTime.UnixEpoch.AddSeconds(t);

                graphics.DrawString(unixTime.ToString("HH:mm:ss"), font, Brushes.Black, new Point((int)(xCoord - font.Size * 2.5f), height - padding + (int)(font.Height * 0.75f)));
                t += TimeRange / XUnitsCount;
            }
        }
    }
}
