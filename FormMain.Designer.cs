namespace _2_convex_hull
{
    partial class FormMain
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
            this.pictureBoxView = new System.Windows.Forms.PictureBox();
            this.buttonGenerate = new System.Windows.Forms.Button();
            this.buttonSolve = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxNumPoints = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.buttonClearToPoints = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.radioUniform = new System.Windows.Forms.RadioButton();
            this.radioGaussian = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxView)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxView
            // 
            this.pictureBoxView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxView.BackColor = System.Drawing.Color.White;
            this.pictureBoxView.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxView.Name = "pictureBoxView";
            this.pictureBoxView.Size = new System.Drawing.Size(698, 450);
            this.pictureBoxView.TabIndex = 0;
            this.pictureBoxView.TabStop = false;
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGenerate.Location = new System.Drawing.Point(297, 471);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(73, 23);
            this.buttonGenerate.TabIndex = 1;
            this.buttonGenerate.Text = "Generate";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
            // 
            // buttonSolve
            // 
            this.buttonSolve.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSolve.Location = new System.Drawing.Point(378, 471);
            this.buttonSolve.Name = "buttonSolve";
            this.buttonSolve.Size = new System.Drawing.Size(73, 23);
            this.buttonSolve.TabIndex = 2;
            this.buttonSolve.Text = "Solve";
            this.buttonSolve.UseVisualStyleBackColor = true;
            this.buttonSolve.Click += new System.EventHandler(this.buttonSolve_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 476);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Number of points to generate:";
            // 
            // textBoxNumPoints
            // 
            this.textBoxNumPoints.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxNumPoints.Location = new System.Drawing.Point(162, 473);
            this.textBoxNumPoints.Name = "textBoxNumPoints";
            this.textBoxNumPoints.Size = new System.Drawing.Size(113, 20);
            this.textBoxNumPoints.TabIndex = 4;
            this.textBoxNumPoints.Text = "10";
            this.textBoxNumPoints.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxNumPoints_Validating);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 526);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(720, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(39, 17);
            this.statusLabel.Text = "Ready";
            // 
            // buttonClearToPoints
            // 
            this.buttonClearToPoints.Location = new System.Drawing.Point(457, 471);
            this.buttonClearToPoints.Name = "buttonClearToPoints";
            this.buttonClearToPoints.Size = new System.Drawing.Size(91, 23);
            this.buttonClearToPoints.TabIndex = 6;
            this.buttonClearToPoints.Text = "Clear To Points";
            this.buttonClearToPoints.UseVisualStyleBackColor = true;
            this.buttonClearToPoints.Click += new System.EventHandler(this.buttonClearToPoints_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 496);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(159, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Distribution of generated points: ";
            // 
            // radioUniform
            // 
            this.radioUniform.AutoSize = true;
            this.radioUniform.Location = new System.Drawing.Point(174, 496);
            this.radioUniform.Name = "radioUniform";
            this.radioUniform.Size = new System.Drawing.Size(86, 17);
            this.radioUniform.TabIndex = 9;
            this.radioUniform.TabStop = true;
            this.radioUniform.Text = "Uniform Oval";
            this.radioUniform.UseVisualStyleBackColor = true;
            this.radioUniform.CheckedChanged += new System.EventHandler(this.radioUniform_CheckedChanged);
            // 
            // radioGaussian
            // 
            this.radioGaussian.AutoSize = true;
            this.radioGaussian.Location = new System.Drawing.Point(266, 496);
            this.radioGaussian.Name = "radioGaussian";
            this.radioGaussian.Size = new System.Drawing.Size(69, 17);
            this.radioGaussian.TabIndex = 10;
            this.radioGaussian.TabStop = true;
            this.radioGaussian.Text = "Gaussian";
            this.radioGaussian.UseVisualStyleBackColor = true;
            this.radioGaussian.CheckedChanged += new System.EventHandler(this.radioGaussian_CheckedChanged);
            // 
            // FormMain
            // 
            this.AcceptButton = this.buttonGenerate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 548);
            this.Controls.Add(this.radioGaussian);
            this.Controls.Add(this.radioUniform);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonClearToPoints);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.textBoxNumPoints);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonSolve);
            this.Controls.Add(this.buttonGenerate);
            this.Controls.Add(this.pictureBoxView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(730, 580);
            this.MinimumSize = new System.Drawing.Size(730, 580);
            this.Name = "FormMain";
            this.Text = "Convex Hull Lab";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxView)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxView;
        private System.Windows.Forms.Button buttonGenerate;
        private System.Windows.Forms.Button buttonSolve;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxNumPoints;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.Button buttonClearToPoints;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radioUniform;
        private System.Windows.Forms.RadioButton radioGaussian;
    }
}

