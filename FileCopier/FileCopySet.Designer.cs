namespace FileCopier
    {
    partial class FileCopySet
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
            {
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lnkSource = new System.Windows.Forms.LinkLabel();
            this.lnkDest = new System.Windows.Forms.LinkLabel();
            this.lblName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(22, 76);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(152, 31);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "#STATUS#";
            // 
            // lnkSource
            // 
            this.lnkSource.AutoSize = true;
            this.lnkSource.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkSource.Location = new System.Drawing.Point(22, 126);
            this.lnkSource.Name = "lnkSource";
            this.lnkSource.Size = new System.Drawing.Size(161, 31);
            this.lnkSource.TabIndex = 4;
            this.lnkSource.TabStop = true;
            this.lnkSource.Text = "#SOURCE#\r\n";
            // 
            // lnkDest
            // 
            this.lnkDest.AutoSize = true;
            this.lnkDest.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkDest.Location = new System.Drawing.Point(22, 176);
            this.lnkDest.Name = "lnkDest";
            this.lnkDest.Size = new System.Drawing.Size(117, 31);
            this.lnkDest.TabIndex = 5;
            this.lnkDest.TabStop = true;
            this.lnkDest.Text = "#DEST#";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(22, 26);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(128, 31);
            this.lblName.TabIndex = 6;
            this.lblName.Text = "#NAME#";
            // 
            // FileCopySet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lnkDest);
            this.Controls.Add(this.lnkSource);
            this.Controls.Add(this.lblStatus);
            this.Name = "FileCopySet";
            this.Size = new System.Drawing.Size(1274, 230);
            this.ResumeLayout(false);
            this.PerformLayout();

            }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.LinkLabel lnkSource;
        private System.Windows.Forms.LinkLabel lnkDest;
        private System.Windows.Forms.Label lblName;
        }
    }
