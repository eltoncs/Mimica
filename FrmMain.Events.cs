//Handles the events of the main form
namespace Mimica
{
    public partial class FrmMain
    {
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
