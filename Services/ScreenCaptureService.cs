using Mimica.Entities;
using System.Diagnostics.Eventing.Reader;

namespace Mimica.Services
{
    public class ScreenCaptureService : IScreenCaptureService
    {
        private Bitmap? screenshotImg = null;
        private bool stopCapturing = false;

        public Bitmap? GetLastScreenshotImg()
        {
            return screenshotImg;
        }

        public async Task StartScreenshotCapture(
            int ScreenshotCaptureIntervalMs,
            PictureBox? lightIcon = null)
        {
            while (true)
            {
                if (stopCapturing)
                {
                    await Task.Delay(ScreenshotCaptureIntervalMs);
                    continue;
                }

                if (lightIcon != null)
                {
                    lightIcon.Visible = !lightIcon.Visible;
                }

                await Task.Run(() => CaptureScreenShot());
                await Task.Delay(ScreenshotCaptureIntervalMs);
            }
        }

        public async Task ForceScreenshotCapture(Event ev)
        {
            await Task.Run(() => CaptureScreenShot(ev));
        }

        public void StopCapturing()
        {
            this.stopCapturing = true;
        }

        public void StartCapturing()
        {
            this.stopCapturing = false;
        }

        public bool IsCapturing()
        {
            return !this.stopCapturing;
        }

        private void CaptureScreenShot(Event? ev = null)
        {
            try
            {
                this.screenshotImg = new Bitmap(Screen.PrimaryScreen!.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

                using Graphics graphics = Graphics.FromImage(screenshotImg);
                graphics.CopyFromScreen(0, 0, 0, 0, screenshotImg.Size);

                if (ev != null)
                {
                    ev.screenShotImg = screenshotImg;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error taking screenshot: {ex.Message}");
            }
        }
    }
}
