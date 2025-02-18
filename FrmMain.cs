namespace Mimica
{
    using Mimica.Services;
    using Mimica.Extensions;
    using Mimica.Entities;

    public partial class FrmMain : Form
    {
        private Queue<Event> eventQueue = new Queue<Event>();
        private string currentUser = Environment.UserName;

        private readonly IScreenCaptureService screenCaptureService;

        public FrmMain(
            IEventHooksService eventHooksService,
            IScreenCaptureService screenCaptureService,
            IEventLogService eventLogService)
        {
            this.screenCaptureService = screenCaptureService;

            InitializeComponent();

            screenCaptureService.StartScreenshotCapture(
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
            Event lastEvent = this.eventQueue.Last();

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

        private void FrmMain_Load(object sender, EventArgs e)
        {
        }

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
    }
}
