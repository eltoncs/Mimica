using Mimica.Entities;

namespace Mimica.Services
{
    public interface IScreenCaptureService
    {
        Bitmap? GetLastScreenshotImg();

        Task StartScreenshotCapture(
            int ScreenshotCaptureIntervalMs,
            PictureBox? lightIcon = null);

        Task TakeScreenshotNow(Event? ev);

        void PauseCapturing();
        void ResumeCapturing();
        bool IsCapturing();
    }
}