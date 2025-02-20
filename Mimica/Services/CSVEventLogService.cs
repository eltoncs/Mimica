using Mimica.Entities;
using Mimica.Extensions;
using System.Collections.Concurrent;
using System.Drawing.Imaging;

namespace Mimica.Services
{
    /// <summary>
    /// Service to save screenshots and log events to a CSV file.
    /// </summary>
    public class CSVEventLogService : ICSVEventLogService
    {
        private const string IMG_NOT_AVAILABLE = "*not available*";

        private string eventLogsFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Mimica", "EventLogs");
        private string screenshotFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Mimica", "Screenshots");

        /// <summary>
        /// Starts to log events every intervalMs milliseconds.
        /// </summary>
        /// <param name="eventQueue"></param>
        /// <param name="user"></param>
        /// <param name="intervalMs"></param>
        /// <param name="saveIcon"></param>
        /// <returns></returns>
        public async Task StartLogRecording(
            ConcurrentQueue<Event> eventQueue,
            string user,
            int intervalMs = 2000,
            PictureBox? saveIcon = null)
        {
            if (!Directory.Exists(eventLogsFolder))
            {
                Directory.CreateDirectory(eventLogsFolder);
            }

            string eventLogFilePath = Path.Combine(eventLogsFolder, $"eventlog_{DateTime.UtcNow.ToUnixDateStamp()}.csv");
            await Task.Delay(intervalMs);

            while (true)
            {
                if (eventQueue.Count == 0)
                {
                    await Task.Delay(intervalMs);
                    continue;
                }

                try
                {
                    await Task.Run(() => SaveEventLogs(
                        filePath: eventLogFilePath,
                        eventQueue: eventQueue));

                    await Task.Delay(intervalMs);
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Error saving log to file: {ex.Message}");//TODO Must be replaced by a log servervice
                }
            }
        }

        private void SaveEventsScreenshots(ConcurrentQueue<Event> eventQueue)
        {
            if (eventQueue.Count == 0)
            {
                return;
            }

            var queueLock = new object();
            var tempQueue = new ConcurrentQueue<Event>(eventQueue);

            while (eventQueue.Count > 0)
            {
                lock (queueLock)
                {
                    if (eventQueue.TryDequeue(out Event? dequeuedEvent))
                    {
                        if (dequeuedEvent.screenShotImg != null)
                        {
                            dequeuedEvent.ScreenShotPath = this.SaveScreenshotToFile(dequeuedEvent.screenShotImg!);
                        }
                    }
                }                    
            }

            eventQueue = new ConcurrentQueue<Event>(tempQueue);
        }

        private string SaveScreenshotToFile(Bitmap screenshotImg)
        {
            if (screenshotImg == null)
            {
                return IMG_NOT_AVAILABLE;
            }

            try
            {
                if (!Directory.Exists(screenshotFolder))
                {
                    Directory.CreateDirectory(screenshotFolder);
                }

                string screenshotFilePath = Path.Combine(screenshotFolder, $"screenshot_{DateTime.UtcNow.ToUnixTimeStamp()}.png");
                screenshotImg.Save(screenshotFilePath, ImageFormat.Png);

                return screenshotFilePath;
            }
            catch (Exception ex)
            {
                return IMG_NOT_AVAILABLE;
            }
        }

        private void SaveEventLogs(
            string filePath,
            ConcurrentQueue<Event> eventQueue)
        {
            try
            {
                this.SaveEventsScreenshots(eventQueue);
                File.AppendAllLines(filePath, eventQueue.GetCSVLines());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error appending lines to CSV: {ex.Message}");//TODO Must be replaced by a log servervice
            }
        }
    }
}
