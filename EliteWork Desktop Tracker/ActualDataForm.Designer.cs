namespace EliteWork_Desktop_Tracker
{
    partial class ActualDataForm
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
            this._second_timer_lb = new System.Windows.Forms.Label();
            this._tird_timer_lb = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _second_timer_lb
            // 
            this._second_timer_lb.AutoSize = true;
            this._second_timer_lb.BackColor = System.Drawing.Color.Transparent;
            this._second_timer_lb.Font = new System.Drawing.Font("Calibri", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._second_timer_lb.ForeColor = System.Drawing.Color.White;
            this._second_timer_lb.Location = new System.Drawing.Point(61, 0);
            this._second_timer_lb.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._second_timer_lb.Name = "_second_timer_lb";
            this._second_timer_lb.Size = new System.Drawing.Size(192, 78);
            this._second_timer_lb.TabIndex = 38;
            this._second_timer_lb.Text = "label3";
            this._second_timer_lb.Click += new System.EventHandler(this._second_timer_lb_Click);
            // 
            // _tird_timer_lb
            // 
            this._tird_timer_lb.AutoSize = true;
            this._tird_timer_lb.BackColor = System.Drawing.Color.Transparent;
            this._tird_timer_lb.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._tird_timer_lb.ForeColor = System.Drawing.Color.White;
            this._tird_timer_lb.Location = new System.Drawing.Point(118, 74);
            this._tird_timer_lb.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._tird_timer_lb.Name = "_tird_timer_lb";
            this._tird_timer_lb.Size = new System.Drawing.Size(74, 29);
            this._tird_timer_lb.TabIndex = 39;
            this._tird_timer_lb.Text = "label3";
            this._tird_timer_lb.Click += new System.EventHandler(this._tird_timer_lb_Click);
            // 
            // ActualDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::EliteWork_Desktop_Tracker.Properties.Resources.notify_form_stopped;
            this.ClientSize = new System.Drawing.Size(314, 112);
            this.Controls.Add(this._tird_timer_lb);
            this.Controls.Add(this._second_timer_lb);
            this.Name = "ActualDataForm";
            this.ShowInTaskbar = false;
            this.Text = "ActualDataForm";
            this.Click += new System.EventHandler(this.ActualDataForm_Click);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _second_timer_lb;
        private System.Windows.Forms.Label _tird_timer_lb;
    }
}