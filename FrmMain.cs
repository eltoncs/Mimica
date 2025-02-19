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

        public FrmMain(
            IOptions<AppSettings> appSettings,
            IEventHooksService eventHooksService,
            IScreenCaptureService screenCaptureService,
            IEventLogService eventLogService)
        {
            this.appSettings = appSettings.Value;

            var ScreenshotCaptureIntervalMs = int.Parse(this.appSettings.ScreenshotCaptureIntervalMs);
            this.screenCaptureService = screenCaptureService;

            InitializeComponent();

            screenCaptureService.StartScreenshotCapture(
                ScreenshotCaptureIntervalMs: ScreenshotCaptureIntervalMs,
                lightIcon: this.imgStatus);

            eventHooksService.Subscribe(
                ProcessMouseEvents,
                ProcessKeyStrokes);

            eventLogService.StartLogCapture(
                user: this.currentUser,
                eventQueue: this.eventQueue);
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

        private void ProcessKeyStrokes(string keyPressed)
        {
            this.eventQueue.Enqueue(
            new Event()
            {
                TimeStamp = DateTime.UtcNow.ToUnixTimeStamp(),
                EventType = EventType.KeyboardKeyPressed,
                screenShotImg = this.screenCaptureService.GetLastScreenshotImg(),
                KeyPressed = keyPressed
            });

            this.logEventToScreen();
        }

        //Other private methods
        private void logEventToScreen()
        {
            if (this.eventQueue.Count == 0)
            {
                return;
            }

            this.logEventToTextPanel();
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

        private void logEventToTextPanel()
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
            notifyIcon.Visible = false;
            Application.Exit();
        }

        private void ShowApp(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }


        #region Event Handlers
        private void chkTopMost_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = this.chkTopMost.Checked;
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            this.eventQueue.Clear();
            this.lvwEvents.Items.Clear();
            this.lblEventCount.Text = $"{eventQueue.Count} events";
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.ShowApp(sender, e);
        }

        private void FrmMain_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();

                this.notifyIcon.BalloonTipTitle = "Mimica Minimized";
                this.notifyIcon.BalloonTipText = "The application is still running in the background.";
                this.notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
                this.notifyIcon.ShowBalloonTip(3000);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.WindowState = FormWindowState.Minimized;
            }
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowApp(sender, e);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ExitApp(sender, e);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.ExitApp(sender, e);
        }
        #endregion

        private void btnStartStopCapturing_Click(object sender, EventArgs e)
        {
            if (this.screenCaptureService.IsCapturing())
            {
                this.screenCaptureService.StopCapturing();
                this.btnStartStopCapturing.Text = "Start Capturing";
                this.lblStatus.Text = "Paused";
                return;
            }

            this.screenCaptureService.StartCapturing();
            this.btnStartStopCapturing.Text = "Stop Capturing";
            this.lblStatus.Text = "Monitoring";
        }
    }
}
