using AppCore.AppTypes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace musicDownloader
{
    public partial class Form1 : Form
    {
        AppCore.RequestsMonitor reqMonitor = AppCore.RequestsMonitor.Instance;

        public Form1()
        {
            InitializeComponent();
            reqMonitor.LogIn += reqMonitor_LogIn;
            reqMonitor.LoadTracksInfo += reqMonitor_LoadTracksInfo;
            reqMonitor.DownloadTracks += reqMonitor_DownloadTracks;
            reqMonitor.DownloadTracksStatistics += reqMonitor_DownloadTracksStatistics;
            reqMonitor.DownloadTrack += reqMonitor_DownloadTrack;
            reqMonitor.LoadFriendsList += reqMonitor_LoadFriendsList;
            panel1.Visible = false;
        }

        #region AppCoreEventHandlers
        void reqMonitor_LoadFriendsList(object sender, AppCore.EventArgs.FriendsListEventArgs args)
        {
            if (args.Status)
            {
                textBox1.Text = ">>>Friends list has been loaded.\r\n" + textBox1.Text;
                panel3.Controls.Clear();
                panel3.Visible = false;
                Int32 y = 10;
                foreach (var item in args.Friends.OrderByDescending(f => f.TracksCount))
                {
                    var newLabel = new Label();
                    newLabel.Width = 300;
                    newLabel.Height = 25;
                    newLabel.Location = new Point(60, y);
                    newLabel.Text = item.Fullname + "   [" + item.TracksCount + "]";
                    panel3.Controls.Add(newLabel);
                    var newBtn = new Button();
                    newBtn.Width = 50;
                    newBtn.Height = 25;
                    newBtn.Location = new Point(10, y - 5);
                    newBtn.Text = "Load";
                    newBtn.Name = item.FriendId;
                    newBtn.Click += newBtn_Click;
                    panel3.Controls.Add(newBtn);
                    y += 35;
                }
                panel3.Visible = true;
            }
            else
            {
                textBox1.Text = ">>>An error occured while loading friends list.\r\n" + textBox1.Text;
            }
        }

        void reqMonitor_DownloadTracksStatistics(object sender, AppCore.EventArgs.TracksDownloadStatisticEventArgs args)
        {
            progressBar1.Maximum = args.TotalTracks;
            totalLabel.Text = args.TotalTracks.ToString();
            downloadedLabel.Text = args.DownloadedTracks.ToString();
        }

        void reqMonitor_DownloadTracks(object sender, AppCore.EventArgs.TracksDownloadEventArgs args)
        {
            if (args.Status)
            {
                if (args.FailedTracks == 0)
                {
                    textBox1.Text = ">>>Download has been completed without errors. Yay!\r\n" + textBox1.Text;
                }
                else
                {
                    textBox1.Text = ">>>Download has been completed. \r\n" + args.FailedTracks + " errors (sadface).\r\n" + textBox1.Text;
                }
            }
            else
            {
                textBox1.Text = ">>>Download failed. Please check logs. (sadface)\r\n" + textBox1.Text;
            }
        }

        void reqMonitor_DownloadTrack(object sender, AppCore.EventArgs.TrackDownloadEventArgs args)
        {
            progressBar1.Increment(1);
        }

        void reqMonitor_LoadTracksInfo(object sender, AppCore.EventArgs.TracksInfoEventArgs args)
        {
            if (args.Status)
            {
                var y = 50;
                panel1.Visible = true;
                foreach (var c in panel1.Controls.OfType<Label>().ToList())
                {
                    panel1.Controls.Remove(c);
                }
                foreach (var c in panel1.Controls.OfType<CheckBox>().Where(c => c.Name != "checkBox1").ToList())
                {
                    panel1.Controls.Remove(c);
                }
                textBox1.Text = ">>>Found " + args.Tracks.Count() + " tracks.\r\n" + textBox1.Text;
                foreach (var track in args.Tracks)
                {
                    panel1.Controls.Add(
                        new Label()
                        {
                            Text = track.Name,
                            Location = new Point(50, y),
                            Width = 500
                        }
                        );
                    panel1.Controls.Add(
                        new CheckBox()
                        {
                            Name = track.TrackId,
                            Location = new Point(20, y)
                        });
                    y += 30;
                }
            }
            else
            {
                textBox1.Text = ">>>An error occured while loading track list.\r\n" + textBox1.Text;
            }
        }

        void reqMonitor_LogIn(object sender, AppCore.EventArgs.LoginEventArgs args)
        {
            if (args.Status)
            {
                textBox1.Text = ">>>Login with success.\r\n" + textBox1.Text;
                textBox1.Text = ">>>Press \"Music\" button if you want to download your music.\r\n" + textBox1.Text;
                textBox1.Text = ">>>Or press \"friends\" button if you want to download your friends music.\r\n" + textBox1.Text;
                panel2.Visible = true;
            }
            else
            {
                String message = ">>>An error occured,";
                if (args.IsNetworkError)
                {
                    message += " please check your internet connection";
                }
                if (args.IsAuthError)
                {
                    message += " please check your username and password";
                }
                textBox1.Text = message + ".\r\n" + textBox1.Text;
                panel2.Visible = false;
            }
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            reqMonitor.BeginCheckCredentials(loginIn.Text, passIn.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (!String.IsNullOrEmpty(folderBrowserDialog1.SelectedPath))
                {
                    textBox1.Text = ">>>Start downloading all tracks.\r\n" + textBox1.Text;
                    textBox1.Text = ">>>You may drink some coffee while I do my work in background. :)\r\n" + textBox1.Text;
                    var checkedTracks = panel1.Controls.OfType<CheckBox>().Where(c => c.Checked && c.Name != "checkBox1").Select(t => t.Name);
                    button1.Enabled = false;
                    reqMonitor.BeginDownload(checkedTracks, folderBrowserDialog1.SelectedPath);
                }
            }
        }

        void newBtn_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if (btn != null)
            {
                reqMonitor.BeginLoadTracksList(btn.Name);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            var isChecked = checkBox1.Checked;
            var checkboxes = panel1.Controls.OfType<CheckBox>().Where(c => c.Name != "checkbox1");
            foreach (var ch in checkboxes)
            {
                ch.Checked = isChecked;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = ">>>Try to find your tracks...\r\n" + textBox1.Text;
            reqMonitor.BeginLoadTracksList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            reqMonitor.BeginLoadFriendsList();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(folderBrowserDialog1.SelectedPath))
            {
                textBox1.Text = ">>>Adding selected tracks to downloads stack.\r\n" + textBox1.Text;
                textBox1.Text = ">>>You may drink some coffee while I do my work in background. :)\r\n" + textBox1.Text;
                var checkedTracks = panel1.Controls.OfType<CheckBox>().Where(c => c.Checked && c.Name != "checkBox1").Select(t => t.Name);
                button1.Enabled = false;
                reqMonitor.BeginDownload(checkedTracks, folderBrowserDialog1.SelectedPath);
            }
            else
            {
                textBox1.Text = ">>>Please select downloads folder (pressing Load music button).\r\n" + textBox1.Text;
            }
        }
    }
}
