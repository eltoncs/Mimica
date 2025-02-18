using Mimica.Entities;
using Mimica.Extensions;
using System.Drawing.Imaging;

namespace Mimica.Services
{
    public class EventLogService : IEventLogService
    {
        private const string IMG_NOT_AVAILABLE = "*not available*";

        private string eventLogsFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Mimica", "EventLogs");
        private string screenshotFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Mimica", "Screenshots");

        public async Task StartLogCapture(
            Queue<Event> eventQueue,
            string user,
            int intervalMs = 2000,
            PictureBox? saveIcon = null)
        {
            if (!Directory.Exists(eventLogsFolder))
            {
                Directory.CreateDirectory(eventLogsFolder);
            }

            string filePath = Path.Combine(eventLogsFolder, $"eventlog_{DateTime.UtcNow.ToUnixDateStamp()}.csv");

            while (true)
            {
                if (eventQueue.Count == 0)
                {
                    await Task.Delay(intervalMs);
                    continue;
                }

                if (saveIcon != null)
                {
                    saveIcon.Visible = !saveIcon.Visible;
                }

                await Task.Run(() => SaveLogToFile(
                    filePath: filePath,
                    eventQueue: eventQueue));

                await Task.Delay(intervalMs);
            }
        }

        private async Task UpdateEvents(Queue<Event> eventQueue)
        {
            if (eventQueue.Count == 0)
            {
                return;
            }

            foreach (Event ev in eventQueue)
            {
                if (ev.screenShotImg != null)
                {
                    ev.ScreenShotPath = await this.SaveScreenshotToFile(ev.screenShotImg!);
                }
            }
        }

        private async Task<string> SaveScreenshotToFile(Bitmap screenshotImg)
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

                string filePath = Path.Combine(screenshotFolder, $"screenshot_{DateTime.UtcNow.ToUnixTimeStamp()}.png");

                await Task.Run(() => screenshotImg.Save(filePath, ImageFormat.Png));
                return filePath;
            }
            catch (Exception ex)
            {
                return IMG_NOT_AVAILABLE;
            }
        }

        private async Task SaveLogToFile(
            string filePath,
            Queue<Event> eventQueue)
        {
            await this.UpdateEvents(eventQueue);

            try
            {
                File.AppendAllLines(filePath, eventQueue.GetCSVLines(dequeue: true));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error appending lines to CSV: {ex.Message}");
            }
        }
    }
}
