namespace FileCopier
    {
    partial class MainForm
        {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
            {
            this.btnBackupAll = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnBackupAll
            // 
            this.btnBackupAll.Location = new System.Drawing.Point(56, 31);
            this.btnBackupAll.Name = "btnBackupAll";
            this.btnBackupAll.Size = new System.Drawing.Size(338, 67);
            this.btnBackupAll.TabIndex = 0;
            this.btnBackupAll.Text = "Backup Everything";
            this.btnBackupAll.UseVisualStyleBackColor = true;
            this.btnBackupAll.Click += new System.EventHandler(this.btnBackupAll_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(1450, 989);
            this.Controls.Add(this.btnBackupAll);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

            }

        #endregion
        private System.Windows.Forms.Button btnBackupAll;
        }
    }

