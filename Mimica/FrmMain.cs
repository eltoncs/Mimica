//Contains the business logic
//Uses a concurrent queue to store user events
//Consumes various services to capture events, screenshots and log them
namespace Mimica
{
    using Mimica.Services;
    using Mimica.Extensions;
    using Mimica.Entities;
    using System.Windows.Forms;
    using Mimica.Properties;
    using Microsoft.Extensions.Options;
    using System.Collections.Concurrent;

    public partial class FrmMain : Form
    {
        private ConcurrentQueue<Event> eventQueue = new ConcurrentQueue<Event>();
        private string currentUser = Environment.UserName;

        private readonly AppSettings appSettings;
        private readonly IScreenCaptureService screenCaptureService;
        private readonly IEventHooksService eventHooksService;

        public FrmMain(
            IOptions<AppSettings> appSettings,
            IEventHooksService eventHooksService,
            IScreenCaptureService screenCaptureService,
            ICSVEventLogService eventLogService)
        {
            this.appSettings = appSettings.Value;
            this.eventHooksService = eventHooksService;
            this.screenCaptureService = screenCaptureService;

            var screenshotCaptureIntervalMs = int.Parse(this.appSettings.ScreenshotCaptureIntervalMs);            
            var logRecordingIntervalMs = int.Parse(this.appSettings.LogRecordingIntervals);            

            InitializeComponent();

            screenCaptureService.StartScreenshotCapture(
                ScreenshotCaptureIntervalMs: screenshotCaptureIntervalMs,
                lightIcon: this.imgStatus);

            eventLogService.StartLogRecording(
                user: this.currentUser,
                eventQueue: this.eventQueue,
                intervalMs: logRecordingIntervalMs);

            this.eventHooksService.Subscribe(
                ProcessMouseEvents,
                ProcessKeyUp,
                ProcessKeyPress);
        }

        //Event hooks callbacks
        private void ProcessMouseEvents(string mouseEvent)
        {
            if (!this.screenCaptureService.IsCapturing())
            {
                return;
            }

            this.eventQueue.Enqueue(
            new Event()
            {
                TimeStamp = DateTime.UtcNow.ToUnixTimeStamp(),
                EventType = mouseEvent == "Right" ? EventType.MouseClickRight : EventType.MouseClickLeft,
                screenShotImg = this.screenCaptureService.GetLastScreenshotImg()
            });

            this.logEventToScreen();
        }

        private void ProcessKeyPress(string keyPressed)
        {
            if (!this.screenCaptureService.IsCapturing())
            {
                return;
            }

            var newEvent = new Event()
            {
                TimeStamp = DateTime.UtcNow.ToUnixTimeStamp(),
                EventType = EventType.KeyboardKeyPressed,
                KeyPressed = keyPressed
            };

            this.screenCaptureService.TakeScreenshotNow(newEvent);
            this.eventQueue.Enqueue(newEvent);
            this.logEventToScreen();
        }

        private void ProcessKeyUp(string keyPressed)
        {
            if (!this.screenCaptureService.IsCapturing())
            {
                return;
            }

            var newEvent = new Event()
            {
                TimeStamp = DateTime.UtcNow.ToUnixTimeStamp(),
                EventType = EventType.KeyboardKeyPressed,
                KeyPressed = keyPressed
            };

            this.screenCaptureService.TakeScreenshotNow(newEvent);
            this.eventQueue.Enqueue(newEvent);
            this.logEventToScreen();
        }

        //Private methods
        private void logEventToScreen()
        {
            if (this.eventQueue.Count == 0)
            {
                return;
            }

            this.LogEventToListView();
            Event? lastEvent = this.eventQueue.LastOrDefault();

            if (lastEvent == null)
            {
                return;
            }

            if (lastEvent.screenShotImg != null)
            {
                this.imgLastScreenshot.BackgroundImage = lastEvent.screenShotImg;
            }
        }

        private void LogEventToListView()
        {
            if (this.eventQueue.Count == 0)
            {
                return;
            }

            Event lastEvt = this.eventQueue.Last();
            ListViewItem item = new ListViewItem
            {
                Text = "",
                ImageKey = lastEvt.EventType.ToString()
            };

            item.SubItems.Add(lastEvt.TimeStamp.ToDateTime().ToString("MM/dd/yyyy HH:mm:ss.fff"));
            item.SubItems.Add(lastEvt.EventType.ToString());
            item.SubItems.Add(lastEvt.KeyPressed);

            this.lvwEvents.Items.Add(item);

            this.lvwEvents.EnsureVisible(this.lvwEvents.Items.Count - 1);
            this.lblEventCount.Text = $"{this.lvwEvents.Items.Count} events";
        }

        private void ExitApp(object sender, EventArgs e)
        {
            this.eventHooksService.Unsubscribe();
            notifyIcon.Dispose();

            Application.Exit();
        }

        private void ShowApp(object sender, EventArgs e)
        {
            this.Show();
            this.Focus();
            this.WindowState = FormWindowState.Normal;
        }
    }
}
