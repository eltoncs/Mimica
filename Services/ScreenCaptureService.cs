namespace Mimica.Services
{
    public class ScreenCaptureService : IScreenCaptureService
    {
        private Bitmap? screenshotImg = null;        

        public Bitmap? GetLastScreenshotImg()
        {
            return screenshotImg;
        }

        public async Task StartScreenshotCapture(PictureBox? lightIcon = null)
        {
            while (true)
            {
                if (lightIcon != null)
                {
                    lightIcon.Visible = !lightIcon.Visible;
                }

                await Task.Run(() => CaptureScreenShot());
                await Task.Delay(200);
            }
        }

        public async Task ForceScreenshotCapture()
        {
            await Task.Run(() => CaptureScreenShot());
        }

        private void CaptureScreenShot()
        {
            try
            {
                this.screenshotImg = new Bitmap(Screen.PrimaryScreen!.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

                using Graphics graphics = Graphics.FromImage(screenshotImg);
                graphics.CopyFromScreen(0, 0, 0, 0, screenshotImg.Size);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error taking screenshot: {ex.Message}");
            }
        }
    }
}
