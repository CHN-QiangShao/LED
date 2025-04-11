namespace dll_Csharp
{
    partial class FormTips
    {
        /// 【summary】
        /// Required designer variable.
        /// 【/summary】
        private System.ComponentModel.IContainer components = null;

        /// 【summary】
        /// Clean up any resources being used.
        /// 【/summary】
        /// 【param name="disposing"】true if managed resources should be disposed; otherwise, false.【/param】
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// 【summary】
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// 【/summary】
        private void InitializeComponent()
        {
            this.pnTips = new System.Windows.Forms.Panel();
            this.lbTips = new System.Windows.Forms.Label();
            this.pnAction = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnTips.SuspendLayout();
            this.pnAction.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnTips
            // 
            this.pnTips.Controls.Add(this.lbTips);
            this.pnTips.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnTips.Location = new System.Drawing.Point(0, 0);
            this.pnTips.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.pnTips.Name = "pnTips";
            this.pnTips.Size = new System.Drawing.Size(684, 312);
            this.pnTips.TabIndex = 0;
            // 
            // lbTips
            // 
            this.lbTips.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbTips.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbTips.Location = new System.Drawing.Point(0, 0);
            this.lbTips.Name = "lbTips";
            this.lbTips.Size = new System.Drawing.Size(684, 312);
            this.lbTips.TabIndex = 0;
            this.lbTips.Text = "lbTips";
            this.lbTips.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnAction
            // 
            this.pnAction.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnAction.Controls.Add(this.btnClose);
            this.pnAction.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnAction.Location = new System.Drawing.Point(0, 312);
            this.pnAction.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.pnAction.Name = "pnAction";
            this.pnAction.Size = new System.Drawing.Size(684, 50);
            this.pnAction.TabIndex = 2;
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClose.Location = new System.Drawing.Point(300, 9);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 30);
            this.btnClose.TabIndex = 56;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FormTips
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 362);
            this.Controls.Add(this.pnTips);
            this.Controls.Add(this.pnAction);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "FormTips";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormTips";
            this.pnTips.ResumeLayout(false);
            this.pnAction.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnTips;
        private System.Windows.Forms.Panel pnAction;
        private System.Windows.Forms.Label lbTips;
        private System.Windows.Forms.Button btnClose;

    }
}