namespace WindowsFormsApp1
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
            this.noFocusCueButton2 = new WindowsFormsApp1.NoFocusCueButton();
            this.noFocusCueButton3 = new WindowsFormsApp1.NoFocusCueButton();
            this.SuspendLayout();
            // 
            // noFocusCueButton2
            // 
            this.noFocusCueButton2.Location = new System.Drawing.Point(229, 250);
            this.noFocusCueButton2.Name = "noFocusCueButton2";
            this.noFocusCueButton2.Size = new System.Drawing.Size(75, 23);
            this.noFocusCueButton2.TabIndex = 1;
            this.noFocusCueButton2.Text = "noFocusCueButton2";
            this.noFocusCueButton2.UseVisualStyleBackColor = true;
            // 
            // noFocusCueButton3
            // 
            this.noFocusCueButton3.Location = new System.Drawing.Point(248, 306);
            this.noFocusCueButton3.Name = "noFocusCueButton3";
            this.noFocusCueButton3.Size = new System.Drawing.Size(75, 23);
            this.noFocusCueButton3.TabIndex = 2;
            this.noFocusCueButton3.Text = "noFocusCueButton3";
            this.noFocusCueButton3.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.noFocusCueButton3);
            this.Controls.Add(this.noFocusCueButton2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private NoFocusCueButton noFocusCueButton2;
        private NoFocusCueButton noFocusCueButton3;
    }
}

