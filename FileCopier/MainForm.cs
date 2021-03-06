﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using FileCopier.Filesystem;
using log4net;

namespace FileCopier
    {
    public partial class MainForm : Form
        {

        private static ILog LOG = LogManager.GetLogger(typeof(MainForm));
        private List<FileCopySet> _copySetControls = new List<FileCopySet>();
        private IFileSystem _fileSystem = new FileSystem();

        public MainForm()
            {
            InitializeComponent();
            Text = "File Copier Tool";
            }

        private void MainForm_Load(object sender, EventArgs e)
            {
            LOG.Info("Loading application");
            var json = new JavaScriptSerializer();
            try
                {
                var jsonText = string.Join(Environment.NewLine, File.ReadAllLines("config.json"));
                var mappings = json.Deserialize<List<DirectoryMapping>>(jsonText);

                // todo: check for duplicates

                _copySetControls = mappings.Select((x, index) => new FileCopySet(left: 20, top: 80 + (index * 130), mapping: x, fileSystem: _fileSystem)).ToList();
                Controls.AddRange(_copySetControls.ToArray());
                }
            catch (Exception ex)
                {
                MessageBox.Show($"Error:{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LOG.Error(ex);
                }
            }

        private void btnCopyAll_Click(object sender, EventArgs e)
            {
            _fileSystem.SetEnabledStatus(enabled: true);
            _copySetControls.ForEach(x => x.BeginCopy());
            }

        private void openLogFileToolStripMenuItem_Click(object sender, EventArgs e)
            {
            Process.Start("CopyToolLog.txt");
            }
        }
    }
