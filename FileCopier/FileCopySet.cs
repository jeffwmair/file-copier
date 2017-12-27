using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Diagnostics;
using log4net;

namespace FileCopier
    {
    public partial class FileCopySet : UserControl
        {

        private static ILog LOG = LogManager.GetLogger(typeof(FileCopySet));

        internal DirectoryMapping Mapping { get; }
        private readonly BackgroundWorker _copyWorker = new BackgroundWorker();
        private readonly BackgroundWorker _copyProgressWorker = new BackgroundWorker();
        private bool _cancelProgressWorker = false;
        private Copier _copier;

        public FileCopySet(int left, int top, DirectoryMapping mapping)
            {
            InitializeComponent();
            Top = top;
            Left = left;
            Mapping = mapping;
            lblStatus.Text = "Ready";
            lnkSource.Text = $"From computer: {mapping.Src}";
            lnkSource.Click += LnkSource_Click;
            lnkDest.Text = $"To destination: {mapping.Dest}";
            lnkDest.Click += LnkDest_Click;
            lblName.Text = mapping.Name;
            _copyWorker.DoWork += Worker_DoWork;
            _copyWorker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            _copyProgressWorker.DoWork += ProgressWorker_DoWork;
            _copier = new Copier(mapping);
            }

        public void BeginCopy()
            {
            _copyWorker.RunWorkerAsync();
            }

        private void LnkDest_Click(object sender, EventArgs e)
            {
            DirectoryInfo dest = new DirectoryInfo(Mapping.Dest);
            if (!dest.Exists)
                {
                try
                    {
                    dest.Create();
                    }
                catch(DirectoryNotFoundException ex)
                    {
                    var msg = $"Couldn't go to the destination directory.  Make sure the USB stick is plugged in.  Error message:{ex.Message}";
                    LOG.Error(msg, ex);
                    MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                    }
                }
            Process.Start(Mapping.Dest);
            }

        private void LnkSource_Click(object sender, EventArgs e)
            {
            Process.Start(Mapping.Src);
            }

        private void BtnCopy_Click(object sender, EventArgs e)
            {
            _copyWorker.RunWorkerAsync();
            }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
            {
            _copyProgressWorker.RunWorkerAsync();
            try
                {
                _copier.StartCopy();
                }
            catch (Exception ex)
                {
                LOG.Error(ex);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        private void ProgressWorker_DoWork(object sender, DoWorkEventArgs e)
            {
            Action SetStatusBackingUp = () => { lblStatus.Text = "Backing up files"; };
            Action AddDotToStatus = () => { lblStatus.Text += "="; };
            Invoke(SetStatusBackingUp);
            int count = 0;
            while (true && !GetCancelProgressWorker())
                {
                if (count >= 50)
                    {
                    Invoke(SetStatusBackingUp);
                    count = 0;
                    }
                Invoke(AddDotToStatus);
                count++;
                Thread.Sleep(50);
                }

            SetCancelProgressWorker(false);
            }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
            {
            SetCancelProgressWorker(true);
            Invoke(new Action(() =>
                {
                    lblStatus.Text = $"Copy completed at {DateTime.Now}";
                }));
            }

        private bool GetCancelProgressWorker()
            {
            lock (this) { return _cancelProgressWorker; }
            }

        private void SetCancelProgressWorker(bool value)
            {
            lock (this) { _cancelProgressWorker = value; }
            }

        }
    }
