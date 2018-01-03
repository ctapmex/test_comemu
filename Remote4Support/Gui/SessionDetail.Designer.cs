namespace Remote4Support.Gui
{
    partial class SessionDetail
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
            this.sessionDetailPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // sessionDetailPropertyGrid
            // 
            this.sessionDetailPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sessionDetailPropertyGrid.LineColor = System.Drawing.SystemColors.ControlDark;
            this.sessionDetailPropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.sessionDetailPropertyGrid.Margin = new System.Windows.Forms.Padding(4);
            this.sessionDetailPropertyGrid.Name = "sessionDetailPropertyGrid";
            this.sessionDetailPropertyGrid.Size = new System.Drawing.Size(379, 321);
            this.sessionDetailPropertyGrid.TabIndex = 0;
            // 
            // SessionDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 321);
            this.Controls.Add(this.sessionDetailPropertyGrid);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SessionDetail";
            this.Text = "Session Detail";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SessionDetail_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid sessionDetailPropertyGrid;
    }
}