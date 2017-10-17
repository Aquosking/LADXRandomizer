namespace LADXRandomizer
{
    partial class Form1
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
            this.btnWriteTest = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.labelLoadedROM = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnWriteTest
            // 
            this.btnWriteTest.Location = new System.Drawing.Point(246, 118);
            this.btnWriteTest.Name = "btnWriteTest";
            this.btnWriteTest.Size = new System.Drawing.Size(138, 38);
            this.btnWriteTest.TabIndex = 0;
            this.btnWriteTest.Text = "Test Modification";
            this.btnWriteTest.UseVisualStyleBackColor = true;
            this.btnWriteTest.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(22, 23);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "Open ROM";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // labelLoadedROM
            // 
            this.labelLoadedROM.AutoSize = true;
            this.labelLoadedROM.Location = new System.Drawing.Point(26, 49);
            this.labelLoadedROM.Name = "labelLoadedROM";
            this.labelLoadedROM.Size = new System.Drawing.Size(71, 13);
            this.labelLoadedROM.TabIndex = 2;
            this.labelLoadedROM.Text = "Loaded Rom:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 261);
            this.Controls.Add(this.labelLoadedROM);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.btnWriteTest);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnWriteTest;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Label labelLoadedROM;
    }
}

