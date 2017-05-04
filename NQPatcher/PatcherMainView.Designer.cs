namespace NQPatcher
{
    partial class PatcherMainView
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
            this.label1 = new System.Windows.Forms.Label();
            this.Civ5InstallFolderTxt = new System.Windows.Forms.TextBox();
            this.ManualFolderSetup = new System.Windows.Forms.Button();
            this.ResetCiv5Folder = new System.Windows.Forms.Button();
            this.PatchButton = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Civ 5 install dir:";
            // 
            // Civ5InstallFolderTxt
            // 
            this.Civ5InstallFolderTxt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Civ5InstallFolderTxt.Location = new System.Drawing.Point(96, 10);
            this.Civ5InstallFolderTxt.Name = "Civ5InstallFolderTxt";
            this.Civ5InstallFolderTxt.Size = new System.Drawing.Size(401, 20);
            this.Civ5InstallFolderTxt.TabIndex = 1;
            // 
            // ManualFolderSetup
            // 
            this.ManualFolderSetup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ManualFolderSetup.Location = new System.Drawing.Point(503, 8);
            this.ManualFolderSetup.Name = "ManualFolderSetup";
            this.ManualFolderSetup.Size = new System.Drawing.Size(24, 23);
            this.ManualFolderSetup.TabIndex = 2;
            this.ManualFolderSetup.Text = "...";
            this.ManualFolderSetup.UseVisualStyleBackColor = true;
            this.ManualFolderSetup.Click += new System.EventHandler(this.ManualFolderSetup_Click);
            // 
            // ResetCiv5Folder
            // 
            this.ResetCiv5Folder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ResetCiv5Folder.Location = new System.Drawing.Point(533, 8);
            this.ResetCiv5Folder.Name = "ResetCiv5Folder";
            this.ResetCiv5Folder.Size = new System.Drawing.Size(56, 23);
            this.ResetCiv5Folder.TabIndex = 3;
            this.ResetCiv5Folder.Text = "Reset";
            this.ResetCiv5Folder.UseVisualStyleBackColor = true;
            this.ResetCiv5Folder.Click += new System.EventHandler(this.ResetCiv5Folder_Click);
            // 
            // PatchButton
            // 
            this.PatchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PatchButton.Location = new System.Drawing.Point(503, 104);
            this.PatchButton.Name = "PatchButton";
            this.PatchButton.Size = new System.Drawing.Size(86, 23);
            this.PatchButton.TabIndex = 4;
            this.PatchButton.Text = "Patch !";
            this.PatchButton.UseVisualStyleBackColor = true;
            this.PatchButton.Click += new System.EventHandler(this.PatchButton_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(12, 133);
            this.progressBar1.MarqueeAnimationSpeed = 50;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(577, 23);
            this.progressBar1.Step = 1;
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 5;
            this.progressBar1.Visible = false;
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Location = new System.Drawing.Point(12, 109);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(0, 13);
            this.StatusLabel.TabIndex = 6;
            this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PatcherMainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 271);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.PatchButton);
            this.Controls.Add(this.ResetCiv5Folder);
            this.Controls.Add(this.ManualFolderSetup);
            this.Controls.Add(this.Civ5InstallFolderTxt);
            this.Controls.Add(this.label1);
            this.Name = "PatcherMainView";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.PatcherMainView_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Civ5InstallFolderTxt;
        private System.Windows.Forms.Button ManualFolderSetup;
        private System.Windows.Forms.Button ResetCiv5Folder;
        private System.Windows.Forms.Button PatchButton;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label StatusLabel;
    }
}

