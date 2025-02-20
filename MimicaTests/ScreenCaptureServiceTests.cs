using Mimica.Services;

namespace Mimica.Tests
{
    [TestClass]
    public class ScreenCaptureServiceTests
    {
        private ScreenCaptureService? captureService;

        [TestInitialize]
        public void Setup()
        {
            captureService = new ScreenCaptureService();
        }

        [TestMethod]
        public async Task TakeScreenshotNow_ShouldCaptureScreenshot()
        {
            var task = captureService!.StartScreenshotCapture(100, null);
            await Task.Delay(150);

            var result = captureService.GetLastScreenshotImg();
            captureService.StopCapturing();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task PauseCapturing_ShouldPauseScreenshotCapture()
        {
            var task = captureService!.StartScreenshotCapture(100, null);
            await Task.Delay(150);

            captureService.PauseCapturing();
            Assert.IsFalse(captureService.IsCapturing());

            captureService.StopCapturing();
        }

        [TestMethod]
        public async Task StopCapturing_ShouldStopScreenshotCapture()
        {
            var task = captureService!.StartScreenshotCapture(100, null);
            await Task.Delay(150);

            captureService.StopCapturing();
            Assert.IsFalse(captureService.IsCapturing());
        }

        [TestMethod]
        public async Task StartCapturing_ShouldResumeScreenshotCapture()
        {
            var task = captureService!.StartScreenshotCapture(100, null);
            await Task.Delay(150);
            Assert.IsTrue(captureService.IsCapturing());

            captureService.PauseCapturing();
            await Task.Delay(150);
            Assert.IsFalse(captureService.IsCapturing());

            captureService.ResumeCapturing();
            await Task.Delay(150);
            Assert.IsTrue(captureService.IsCapturing());
        }
    }
}
