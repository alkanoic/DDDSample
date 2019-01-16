namespace DDDSample.WinForms
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.Btn_SubForm = new System.Windows.Forms.Button();
            this.Btn_UserInsert = new System.Windows.Forms.Button();
            this.Btn_ErrorLog = new System.Windows.Forms.Button();
            this.Btn_UserSelect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Btn_SubForm
            // 
            this.Btn_SubForm.Location = new System.Drawing.Point(12, 12);
            this.Btn_SubForm.Name = "Btn_SubForm";
            this.Btn_SubForm.Size = new System.Drawing.Size(75, 23);
            this.Btn_SubForm.TabIndex = 0;
            this.Btn_SubForm.Text = "SubOpen";
            this.Btn_SubForm.UseVisualStyleBackColor = true;
            this.Btn_SubForm.Click += new System.EventHandler(this.Btn_SubForm_Click);
            // 
            // Btn_UserInsert
            // 
            this.Btn_UserInsert.Location = new System.Drawing.Point(12, 41);
            this.Btn_UserInsert.Name = "Btn_UserInsert";
            this.Btn_UserInsert.Size = new System.Drawing.Size(75, 23);
            this.Btn_UserInsert.TabIndex = 1;
            this.Btn_UserInsert.Text = "Insert";
            this.Btn_UserInsert.UseVisualStyleBackColor = true;
            this.Btn_UserInsert.Click += new System.EventHandler(this.Btn_UserInsert_Click);
            // 
            // Btn_ErrorLog
            // 
            this.Btn_ErrorLog.Location = new System.Drawing.Point(12, 70);
            this.Btn_ErrorLog.Name = "Btn_ErrorLog";
            this.Btn_ErrorLog.Size = new System.Drawing.Size(75, 23);
            this.Btn_ErrorLog.TabIndex = 2;
            this.Btn_ErrorLog.Text = "ErrorLog";
            this.Btn_ErrorLog.UseVisualStyleBackColor = true;
            this.Btn_ErrorLog.Click += new System.EventHandler(this.Btn_ErrorLog_Click);
            // 
            // Btn_UserSelect
            // 
            this.Btn_UserSelect.Location = new System.Drawing.Point(101, 41);
            this.Btn_UserSelect.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.Btn_UserSelect.Name = "Btn_UserSelect";
            this.Btn_UserSelect.Size = new System.Drawing.Size(74, 23);
            this.Btn_UserSelect.TabIndex = 3;
            this.Btn_UserSelect.Text = "Select";
            this.Btn_UserSelect.UseVisualStyleBackColor = true;
            this.Btn_UserSelect.Click += new System.EventHandler(this.Btn_UserSelect_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 297);
            this.Controls.Add(this.Btn_UserSelect);
            this.Controls.Add(this.Btn_ErrorLog);
            this.Controls.Add(this.Btn_UserInsert);
            this.Controls.Add(this.Btn_SubForm);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Btn_SubForm;
        private System.Windows.Forms.Button Btn_UserInsert;
        private System.Windows.Forms.Button Btn_ErrorLog;
        private System.Windows.Forms.Button Btn_UserSelect;
    }
}

