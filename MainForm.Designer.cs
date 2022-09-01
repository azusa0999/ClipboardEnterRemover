namespace ClipboardEnterRemover
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
            this.btnClipboardGetText = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxRemoveTagetVal = new System.Windows.Forms.TextBox();
            this.lbxLog = new System.Windows.Forms.ListBox();
            this.prbStatus = new System.Windows.Forms.ProgressBar();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClipboardGetText
            // 
            this.btnClipboardGetText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClipboardGetText.Location = new System.Drawing.Point(163, 235);
            this.btnClipboardGetText.Name = "btnClipboardGetText";
            this.btnClipboardGetText.Size = new System.Drawing.Size(163, 32);
            this.btnClipboardGetText.TabIndex = 1;
            this.btnClipboardGetText.Text = "시작하기";
            this.btnClipboardGetText.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 79F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnClipboardGetText, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbxRemoveTagetVal, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbxLog, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.prbStatus, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(329, 270);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 30);
            this.label1.TabIndex = 2;
            this.label1.Text = "제거문자 :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbxRemoveTagetVal
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.tbxRemoveTagetVal, 2);
            this.tbxRemoveTagetVal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxRemoveTagetVal.Location = new System.Drawing.Point(82, 3);
            this.tbxRemoveTagetVal.Name = "tbxRemoveTagetVal";
            this.tbxRemoveTagetVal.ReadOnly = true;
            this.tbxRemoveTagetVal.Size = new System.Drawing.Size(244, 23);
            this.tbxRemoveTagetVal.TabIndex = 3;
            this.tbxRemoveTagetVal.Text = "\\r;\\n;-\\r;-\\n";
            // 
            // lbxLog
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.lbxLog, 3);
            this.lbxLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxLog.FormattingEnabled = true;
            this.lbxLog.ItemHeight = 15;
            this.lbxLog.Location = new System.Drawing.Point(3, 33);
            this.lbxLog.Name = "lbxLog";
            this.lbxLog.Size = new System.Drawing.Size(323, 196);
            this.lbxLog.TabIndex = 4;
            // 
            // prbStatus
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.prbStatus, 2);
            this.prbStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.prbStatus.Location = new System.Drawing.Point(3, 235);
            this.prbStatus.Name = "prbStatus";
            this.prbStatus.Size = new System.Drawing.Size(154, 32);
            this.prbStatus.TabIndex = 5;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 270);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
            this.Text = "클립보드 엔터 제거기";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Button btnClipboardGetText;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private TextBox tbxRemoveTagetVal;
        private ListBox lbxLog;
        private ProgressBar prbStatus;
    }
}