using Mimica.Entities;

namespace Mimica.Services
{
    /// <summary>
    /// Capture screenshots every ScreenshotCaptureIntervalMs milliseconds.
    /// </summary>
    public class ScreenCaptureService : IScreenCaptureService
    {
        private Bitmap? screenshotImg = null;
        private bool stopCapturing = false;

        public Bitmap? GetLastScreenshotImg()
        {
            return screenshotImg;
        }

        /// <summary>
        /// Start capturing screenshots every ScreenshotCaptureIntervalMs milliseconds.
        /// </summary>
        /// <param name="ScreenshotCaptureIntervalMs"></param>
        /// <param name="lightIcon"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Imediately take a screenshot and update the event with it.
        /// </summary>
        /// <param name="ev"></param>
        /// <returns></returns>
        public async Task TakeScreenshotNow(Event? ev)
        {
            if (ev == null)
            {
                return;
            }

            await Task.Run(() => CaptureScreenShot(ev));
        }

        /// <summary>
        /// Stop capturing screenshots.
        /// </summary>
        public void StopCapturing()
        {
            this.stopCapturing = true;
        }

        /// <summary>
        /// Resume capturing screenshots.
        /// </summary>
        public void StartCapturing()
        {
            this.stopCapturing = false;
        }

        /// <summary>
        /// Check if the service is capturing screenshots.
        /// </summary>
        /// <returns></returns>
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
