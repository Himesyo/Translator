
namespace Himesyo.DocumentTranslator
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuBtnOpenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.documentBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ucFileMain = new Himesyo.DocumentTranslator.UcFileBox();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.documentBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuBtnOpenFile});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(128, 26);
            // 
            // menuBtnOpenFile
            // 
            this.menuBtnOpenFile.Name = "menuBtnOpenFile";
            this.menuBtnOpenFile.Size = new System.Drawing.Size(127, 22);
            this.menuBtnOpenFile.Text = "打开(&O)...";
            this.menuBtnOpenFile.Click += new System.EventHandler(this.menuBtnOpenFile_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // documentBindingSource
            // 
            this.documentBindingSource.DataSource = typeof(Himesyo.DocumentTranslator.Document);
            // 
            // ucFileMain
            // 
            this.ucFileMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucFileMain.Location = new System.Drawing.Point(14, 14);
            this.ucFileMain.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.ucFileMain.Name = "ucFileMain";
            this.ucFileMain.Size = new System.Drawing.Size(1191, 606);
            this.ucFileMain.TabIndex = 1;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1219, 634);
            this.Controls.Add(this.ucFileMain);
            this.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "文档翻译";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.documentBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource documentBindingSource;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuBtnOpenFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private UcFileBox ucFileMain;
    }
}

