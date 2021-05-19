
namespace PPR_Annotationstool
{
    partial class MainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.DateitoolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ÖffnentoolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.BearbeitentoolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.AnsichttoolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.HilfetoolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DateitoolStripMenuItem1,
            this.BearbeitentoolStripMenuItem1,
            this.AnsichttoolStripMenuItem2,
            this.HilfetoolStripMenuItem3});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // DateitoolStripMenuItem1
            // 
            this.DateitoolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ÖffnentoolStripMenuItem1});
            this.DateitoolStripMenuItem1.Name = "DateitoolStripMenuItem1";
            this.DateitoolStripMenuItem1.Size = new System.Drawing.Size(59, 24);
            this.DateitoolStripMenuItem1.Text = "Datei";
            // 
            // ÖffnentoolStripMenuItem1
            // 
            this.ÖffnentoolStripMenuItem1.Name = "ÖffnentoolStripMenuItem1";
            this.ÖffnentoolStripMenuItem1.Size = new System.Drawing.Size(137, 26);
            this.ÖffnentoolStripMenuItem1.Text = "Öffnen";
            this.ÖffnentoolStripMenuItem1.Click += new System.EventHandler(this.ÖffnentoolStripMenuItem1_Click);
            // 
            // BearbeitentoolStripMenuItem1
            // 
            this.BearbeitentoolStripMenuItem1.Name = "BearbeitentoolStripMenuItem1";
            this.BearbeitentoolStripMenuItem1.Size = new System.Drawing.Size(95, 24);
            this.BearbeitentoolStripMenuItem1.Text = "Bearbeiten";
            // 
            // AnsichttoolStripMenuItem2
            // 
            this.AnsichttoolStripMenuItem2.Name = "AnsichttoolStripMenuItem2";
            this.AnsichttoolStripMenuItem2.Size = new System.Drawing.Size(71, 24);
            this.AnsichttoolStripMenuItem2.Text = "Ansicht";
            // 
            // HilfetoolStripMenuItem3
            // 
            this.HilfetoolStripMenuItem3.Name = "HilfetoolStripMenuItem3";
            this.HilfetoolStripMenuItem3.Size = new System.Drawing.Size(55, 24);
            this.HilfetoolStripMenuItem3.Text = "Hilfe";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 28);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(800, 400);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Images|*.bmp;*.png;*.jpg";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Annotationstool";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem DateitoolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem BearbeitentoolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem AnsichttoolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem HilfetoolStripMenuItem3;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem ÖffnentoolStripMenuItem1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

