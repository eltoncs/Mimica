namespace Mimica.Services
{
    public interface IScreenCaptureService
    {
        Bitmap? GetLastScreenshotImg();
        Task StartScreenshotCapture(PictureBox? lightIcon = null);
        Task ForceScreenshotCapture();
    }
}