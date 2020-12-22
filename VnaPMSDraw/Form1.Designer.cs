namespace VnaPMSDraw
{
    partial class BackForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.processImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportHTMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eXPORTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iMPORTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.processImageToolStripMenuItem,
            this.exportHTMLToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1845, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // processImageToolStripMenuItem
            // 
            this.processImageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadImageToolStripMenuItem});
            this.processImageToolStripMenuItem.Name = "processImageToolStripMenuItem";
            this.processImageToolStripMenuItem.Size = new System.Drawing.Size(120, 24);
            this.processImageToolStripMenuItem.Text = "Process Image";
            // 
            // loadImageToolStripMenuItem
            // 
            this.loadImageToolStripMenuItem.Name = "loadImageToolStripMenuItem";
            this.loadImageToolStripMenuItem.Size = new System.Drawing.Size(172, 26);
            this.loadImageToolStripMenuItem.Text = "Load Image";
            this.loadImageToolStripMenuItem.Click += new System.EventHandler(this.loadImageToolStripMenuItem_Click);
            // 
            // exportHTMLToolStripMenuItem
            // 
            this.exportHTMLToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eXPORTToolStripMenuItem,
            this.iMPORTToolStripMenuItem});
            this.exportHTMLToolStripMenuItem.Name = "exportHTMLToolStripMenuItem";
            this.exportHTMLToolStripMenuItem.Size = new System.Drawing.Size(63, 24);
            this.exportHTMLToolStripMenuItem.Text = "HTML";
            // 
            // eXPORTToolStripMenuItem
            // 
            this.eXPORTToolStripMenuItem.Name = "eXPORTToolStripMenuItem";
            this.eXPORTToolStripMenuItem.Size = new System.Drawing.Size(148, 26);
            this.eXPORTToolStripMenuItem.Text = "EXPORT";
            this.eXPORTToolStripMenuItem.Click += new System.EventHandler(this.eXPORTToolStripMenuItem_Click);
            // 
            // iMPORTToolStripMenuItem
            // 
            this.iMPORTToolStripMenuItem.Name = "iMPORTToolStripMenuItem";
            this.iMPORTToolStripMenuItem.Size = new System.Drawing.Size(148, 26);
            this.iMPORTToolStripMenuItem.Text = "IMPORT";
            this.iMPORTToolStripMenuItem.Click += new System.EventHandler(this.iMPORTToolStripMenuItem_Click);
            // 
            // BackForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1845, 783);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "BackForm";
            this.Text = "만들기";
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MouseRigltClick);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem processImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportHTMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eXPORTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iMPORTToolStripMenuItem;
    }
}

