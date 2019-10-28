namespace Quanly.Quanlyvitri
{
    partial class Suavitri
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
            this.label2 = new System.Windows.Forms.Label();
            this.matxtb = new System.Windows.Forms.TextBox();
            this.tentxtb = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã:";
            this.label1.Click += new System.EventHandler(this.Label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tên:";
            // 
            // matxtb
            // 
            this.matxtb.Location = new System.Drawing.Point(76, 29);
            this.matxtb.Name = "matxtb";
            this.matxtb.Size = new System.Drawing.Size(177, 20);
            this.matxtb.TabIndex = 2;
            // 
            // tentxtb
            // 
            this.tentxtb.Location = new System.Drawing.Point(76, 65);
            this.tentxtb.Name = "tentxtb";
            this.tentxtb.Size = new System.Drawing.Size(177, 20);
            this.tentxtb.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(272, 29);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 56);
            this.button1.TabIndex = 4;
            this.button1.Text = "Sửa";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Suavitri
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 115);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tentxtb);
            this.Controls.Add(this.matxtb);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Suavitri";
            this.Text = "Suavitri";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox matxtb;
        private System.Windows.Forms.TextBox tentxtb;
        private System.Windows.Forms.Button button1;
    }
}