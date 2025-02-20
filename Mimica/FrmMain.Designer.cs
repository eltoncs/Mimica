namespace Mimica
{
    partial class FrmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            lblStatus = new Label();
            imgLastScreenshot = new PictureBox();
            imgStatus = new PictureBox();
            lvwEvents = new ListView();
            image = new ColumnHeader();
            eventDate = new ColumnHeader();
            eventType = new ColumnHeader();
            keyPressed = new ColumnHeader();
            imageList1 = new ImageList(components);
            lblEventCount = new Label();
            statusStrip1 = new StatusStrip();
            chkTopMost = new CheckBox();
            cmdClear = new Button();
            notifyIcon = new NotifyIcon(components);
            contextMenuStrip = new ContextMenuStrip(components);
            showToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            resetMonitoringToolStripMenuItem = new ToolStripMenuItem();
            btnExit = new Button();
            btnStartStopCapturing = new Button();
            ((System.ComponentModel.ISupportInitialize)imgLastScreenshot).BeginInit();
            ((System.ComponentModel.ISupportInitialize)imgStatus).BeginInit();
            contextMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(35, 22);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(92, 20);
            lblStatus.TabIndex = 0;
            lblStatus.Text = "Monitoring...";
            // 
            // imgLastScreenshot
            // 
            imgLastScreenshot.BackgroundImageLayout = ImageLayout.Stretch;
            imgLastScreenshot.BorderStyle = BorderStyle.FixedSingle;
            imgLastScreenshot.Location = new Point(17, 48);
            imgLastScreenshot.Name = "imgLastScreenshot";
            imgLastScreenshot.Size = new Size(115, 105);
            imgLastScreenshot.TabIndex = 1;
            imgLastScreenshot.TabStop = false;
            // 
            // imgStatus
            // 
            imgStatus.BackgroundImage = (Image)resources.GetObject("imgStatus.BackgroundImage");
            imgStatus.BackgroundImageLayout = ImageLayout.Stretch;
            imgStatus.Location = new Point(12, 24);
            imgStatus.Name = "imgStatus";
            imgStatus.Size = new Size(19, 17);
            imgStatus.TabIndex = 2;
            imgStatus.TabStop = false;
            imgStatus.Visible = false;
            // 
            // lvwEvents
            // 
            lvwEvents.Columns.AddRange(new ColumnHeader[] { image, eventDate, eventType, keyPressed });
            lvwEvents.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            lvwEvents.Location = new Point(150, 12);
            lvwEvents.MultiSelect = false;
            lvwEvents.Name = "lvwEvents";
            lvwEvents.Size = new Size(523, 221);
            lvwEvents.SmallImageList = imageList1;
            lvwEvents.TabIndex = 4;
            lvwEvents.UseCompatibleStateImageBehavior = false;
            lvwEvents.View = View.Details;
            // 
            // image
            // 
            image.Text = "";
            image.Width = 30;
            // 
            // eventDate
            // 
            eventDate.Text = "Date";
            eventDate.Width = 200;
            // 
            // eventType
            // 
            eventType.Text = "Type";
            eventType.Width = 140;
            // 
            // keyPressed
            // 
            keyPressed.Text = "Key Pressed";
            keyPressed.Width = 120;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            imageList1.TransparentColor = Color.Transparent;
            imageList1.Images.SetKeyName(0, "MouseClickLeft");
            imageList1.Images.SetKeyName(1, "MouseClickRight");
            imageList1.Images.SetKeyName(2, "KeyboardKeyPressed");
            // 
            // lblEventCount
            // 
            lblEventCount.AutoSize = true;
            lblEventCount.Location = new Point(373, 242);
            lblEventCount.Name = "lblEventCount";
            lblEventCount.Size = new Size(63, 20);
            lblEventCount.TabIndex = 5;
            lblEventCount.Text = "0 events";
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Location = new Point(0, 278);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(685, 22);
            statusStrip1.TabIndex = 6;
            statusStrip1.Text = "statusStrip1";
            // 
            // chkTopMost
            // 
            chkTopMost.AutoSize = true;
            chkTopMost.Location = new Point(17, 173);
            chkTopMost.Name = "chkTopMost";
            chkTopMost.Size = new Size(89, 24);
            chkTopMost.TabIndex = 7;
            chkTopMost.Text = "Topmost";
            chkTopMost.UseVisualStyleBackColor = true;
            chkTopMost.CheckedChanged += chkTopMost_CheckedChanged;
            // 
            // cmdClear
            // 
            cmdClear.Location = new Point(150, 239);
            cmdClear.Name = "cmdClear";
            cmdClear.Size = new Size(60, 27);
            cmdClear.TabIndex = 8;
            cmdClear.Text = "Clear";
            cmdClear.UseVisualStyleBackColor = true;
            cmdClear.Click += cmdClear_Click;
            // 
            // notifyIcon
            // 
            notifyIcon.ContextMenuStrip = contextMenuStrip;
            notifyIcon.Icon = (Icon)resources.GetObject("notifyIcon.Icon");
            notifyIcon.Text = "Mimica Recorder (By Elton)";
            notifyIcon.Visible = true;
            notifyIcon.MouseDoubleClick += notifyIcon_MouseDoubleClick;
            // 
            // contextMenuStrip
            // 
            contextMenuStrip.ImageScalingSize = new Size(20, 20);
            contextMenuStrip.Items.AddRange(new ToolStripItem[] { showToolStripMenuItem, exitToolStripMenuItem, resetMonitoringToolStripMenuItem });
            contextMenuStrip.Name = "contextMenuStrip";
            contextMenuStrip.Size = new Size(193, 76);
            // 
            // showToolStripMenuItem
            // 
            showToolStripMenuItem.Name = "showToolStripMenuItem";
            showToolStripMenuItem.Size = new Size(192, 24);
            showToolStripMenuItem.Text = "Show";
            showToolStripMenuItem.Click += showToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(192, 24);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // resetMonitoringToolStripMenuItem
            // 
            resetMonitoringToolStripMenuItem.Name = "resetMonitoringToolStripMenuItem";
            resetMonitoringToolStripMenuItem.Size = new Size(192, 24);
            resetMonitoringToolStripMenuItem.Text = "Reset Monitoring";
            // 
            // btnExit
            // 
            btnExit.Location = new Point(603, 244);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(66, 29);
            btnExit.TabIndex = 9;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // btnStartStopCapturing
            // 
            btnStartStopCapturing.Location = new Point(216, 237);
            btnStartStopCapturing.Name = "btnStartStopCapturing";
            btnStartStopCapturing.Size = new Size(127, 29);
            btnStartStopCapturing.TabIndex = 10;
            btnStartStopCapturing.Text = "Pause Capturing";
            btnStartStopCapturing.UseVisualStyleBackColor = true;
            btnStartStopCapturing.Click += btnStartStopCapturing_Click;
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnExit;
            ClientSize = new Size(685, 300);
            Controls.Add(btnStartStopCapturing);
            Controls.Add(btnExit);
            Controls.Add(cmdClear);
            Controls.Add(chkTopMost);
            Controls.Add(statusStrip1);
            Controls.Add(lblEventCount);
            Controls.Add(lvwEvents);
            Controls.Add(imgStatus);
            Controls.Add(imgLastScreenshot);
            Controls.Add(lblStatus);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimumSize = new Size(400, 184);
            Name = "FrmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Mimica Recorder (By Elton)";
            FormClosing += MainForm_FormClosing;
            Resize += FrmMain_Resize;
            ((System.ComponentModel.ISupportInitialize)imgLastScreenshot).EndInit();
            ((System.ComponentModel.ISupportInitialize)imgStatus).EndInit();
            contextMenuStrip.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblStatus;
        private PictureBox imgLastScreenshot;
        private PictureBox imgStatus;
        private ListView lvwEvents;
        private ImageList imageList1;
        private ColumnHeader image;
        private ColumnHeader eventDate;
        private ColumnHeader eventType;
        private ColumnHeader keyPressed;
        private Label lblEventCount;
        private StatusStrip statusStrip1;
        private CheckBox chkTopMost;
        private Button cmdClear;
        private NotifyIcon notifyIcon;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem showToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private Button btnExit;
        private ToolStripMenuItem resetMonitoringToolStripMenuItem;
        private Button btnStartStopCapturing;
    }
}
