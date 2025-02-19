namespace Mimica.Services
{
    public interface IScreenCaptureService
    {
        Bitmap? GetLastScreenshotImg();

        Task StartScreenshotCapture(
            int ScreenshotCaptureIntervalMs,
            PictureBox? lightIcon = null);

        Task ForceScreenshotCapture();

        void StopCapturing();
        void StartCapturing();
        bool IsCapturing();
    }
}