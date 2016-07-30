namespace GraduationProject
{
    partial class ImageForm
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
            this.btnImageOpen = new System.Windows.Forms.Button();
            this.imgPlanet = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.imgPlanet)).BeginInit();
            this.SuspendLayout();
            // 
            // btnImageOpen
            // 
            this.btnImageOpen.Location = new System.Drawing.Point(337, 12);
            this.btnImageOpen.Name = "btnImageOpen";
            this.btnImageOpen.Size = new System.Drawing.Size(75, 23);
            this.btnImageOpen.TabIndex = 0;
            this.btnImageOpen.Text = "Open";
            this.btnImageOpen.UseVisualStyleBackColor = true;
            this.btnImageOpen.Click += new System.EventHandler(this.btnImageOpen_Click);
            // 
            // imgPlanet
            // 
            this.imgPlanet.Location = new System.Drawing.Point(12, 50);
            this.imgPlanet.Name = "imgPlanet";
            this.imgPlanet.Size = new System.Drawing.Size(784, 672);
            this.imgPlanet.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgPlanet.TabIndex = 1;
            this.imgPlanet.TabStop = false;
            // 
            // ImageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 734);
            this.Controls.Add(this.imgPlanet);
            this.Controls.Add(this.btnImageOpen);
            this.Name = "ImageForm";
            this.Text = "ImageForm";
            ((System.ComponentModel.ISupportInitialize)(this.imgPlanet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnImageOpen;
        private System.Windows.Forms.PictureBox imgPlanet;
    }
}