namespace EliteWork_Desktop_Tracker
{
    partial class CrashReportAlert
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CrashReportAlert));
            this._crash_lb = new System.Windows.Forms.Label();
            this._report_crash_btn = new System.Windows.Forms.Button();
            this._report_crash_manually_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _crash_lb
            // 
            this._crash_lb.AutoSize = true;
            this._crash_lb.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._crash_lb.Location = new System.Drawing.Point(16, 17);
            this._crash_lb.Name = "_crash_lb";
            this._crash_lb.Size = new System.Drawing.Size(431, 72);
            this._crash_lb.TabIndex = 0;
            this._crash_lb.Text = resources.GetString("_crash_lb.Text");
            // 
            // _report_crash_btn
            // 
            this._report_crash_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._report_crash_btn.Location = new System.Drawing.Point(211, 111);
            this._report_crash_btn.Name = "_report_crash_btn";
            this._report_crash_btn.Size = new System.Drawing.Size(110, 27);
            this._report_crash_btn.TabIndex = 1;
            this._report_crash_btn.Text = "Report Now";
            this._report_crash_btn.UseVisualStyleBackColor = true;
            this._report_crash_btn.Click += new System.EventHandler(this._report_crash_btn_Click);
            // 
            // _report_crash_manually_btn
            // 
            this._report_crash_manually_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._report_crash_manually_btn.Location = new System.Drawing.Point(327, 111);
            this._report_crash_manually_btn.Name = "_report_crash_manually_btn";
            this._report_crash_manually_btn.Size = new System.Drawing.Size(110, 27);
            this._report_crash_manually_btn.TabIndex = 2;
            this._report_crash_manually_btn.Text = "Report Later";
            this._report_crash_manually_btn.UseVisualStyleBackColor = true;
            this._report_crash_manually_btn.Click += new System.EventHandler(this._report_crash_manually_btn_Click);
            // 
            // CrashReportAlert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(458, 158);
            this.Controls.Add(this._report_crash_manually_btn);
            this.Controls.Add(this._report_crash_btn);
            this.Controls.Add(this._crash_lb);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CrashReportAlert";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EliteWork: Application Crash Alert";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CrashReportAlert_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _crash_lb;
        private System.Windows.Forms.Button _report_crash_btn;
        private System.Windows.Forms.Button _report_crash_manually_btn;
    }
}