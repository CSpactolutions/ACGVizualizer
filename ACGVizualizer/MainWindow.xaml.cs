using ACGVizualizer.Models;
using ACGVizualizer.Models.QueueEvents;
using ACGVizualizer.Services;
using ACGVizualizer.Services.Parser;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ACGVizualizer
{
    /// <summary>
    /// Represents the main window of the ACGVizualizer application.
    /// </summary>
    public partial class MainWindow : Window
    {
        private BitmapImage _bitmapImage;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            _bitmapImage = new BitmapImage();
            _bitmapImage.BeginInit();
        }

        /// <summary>
        /// Opens a file dialog and returns the path of the selected file.
        /// </summary>
        /// <returns>The path of the selected file, or null if no file was selected.</returns>
        private string? OpenFile()
        {
            var dlg = new OpenFileDialog
            {
                DefaultExt = ".txt",
                Filter = "Txt files (*.txt)|*.txt"
            };
            var result = dlg.ShowDialog();
            return result == true ? dlg.FileName : null;
        }

        /// <summary>
        /// Handles the Load click event. Reads the content of the selected file,
        /// parses the log, creates a call record collection, and exports a bitmap to display.
        /// </summary>
        private void MenuItem_Open_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var lines = OpenFile();
                if (lines != null)
                {
                    var content = File.ReadAllLines(lines);
                    var queueLogParser = new QueueLogParser();
                    var entries = queueLogParser.ParseLog(content);
                    var recordCollection = new CallRecordCollection(entries);
                    var series = new RangeSeries(recordCollection);
                    var bitmap = series.ExportToBitmap(); // choose format

                    using (var memory = new MemoryStream())
                    {
                        bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                        memory.Position = 0;
                        _bitmapImage = new BitmapImage();
                        _bitmapImage.BeginInit();
                        _bitmapImage.StreamSource = memory;
                        _bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                        _bitmapImage.EndInit();
                    }
                    imageControl.Source = _bitmapImage;
                    menuItemSave.IsEnabled = true;
                }
            }
            catch { }
        }

        /// <summary>
        /// Handles the Save click event. Saves the displayed bitmap to a file.
        /// </summary>
        private void MenuItem_Save_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implement code to save the current image
            var dialog = new SaveFileDialog
            {
                FileName = "image",
                DefaultExt = ".png",
                Filter = "PNG files (*.png)|*.png|All files (*.*)|*.*"
            };
            if (dialog.ShowDialog() == true)
            {
                using (var stream = new FileStream(dialog.FileName, FileMode.Create))
                {
                    var encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(_bitmapImage));
                    encoder.Save(stream);
                }
            }
        }
    }
}
