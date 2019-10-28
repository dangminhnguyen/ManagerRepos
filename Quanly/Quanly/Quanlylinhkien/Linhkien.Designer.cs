namespace Quanly.Quanlylinhkien
{
    partial class Linhkien
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
            this.modellk = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tenlk = new System.Windows.Forms.TextBox();
            this.malk = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Taikhoan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Vaitro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // modellk
            // 
            this.modellk.FormattingEnabled = true;
            this.modellk.Location = new System.Drawing.Point(81, 12);
            this.modellk.Name = "modellk";
            this.modellk.Size = new System.Drawing.Size(271, 21);
            this.modellk.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tên Máy:";
            this.label1.Click += new System.EventHandler(this.Label1_Click);
            // 
            // tenlk
            // 
            this.tenlk.Location = new System.Drawing.Point(81, 45);
            this.tenlk.Name = "tenlk";
            this.tenlk.Size = new System.Drawing.Size(271, 20);
            this.tenlk.TabIndex = 3;
            // 
            // malk
            // 
            this.malk.Location = new System.Drawing.Point(429, 12);
            this.malk.Name = "malk";
            this.malk.Size = new System.Drawing.Size(145, 20);
            this.malk.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(399, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "mã:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Tên:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Location = new System.Drawing.Point(12, 75);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(582, 381);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Taikhoan,
            this.Vaitro});
            this.dataGridView1.Location = new System.Drawing.Point(6, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(570, 359);
            this.dataGridView1.TabIndex = 10;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Tên máy";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Taikhoan
            // 
            this.Taikhoan.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Taikhoan.HeaderText = "Mã";
            this.Taikhoan.Name = "Taikhoan";
            this.Taikhoan.ReadOnly = true;
            // 
            // Vaitro
            // 
            this.Vaitro.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Vaitro.HeaderText = "Tên";
            this.Vaitro.Name = "Vaitro";
            this.Vaitro.ReadOnly = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(429, 42);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(145, 24);
            this.button1.TabIndex = 8;
            this.button1.Text = "Add";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Linhkien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 458);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.malk);
            this.Controls.Add(this.tenlk);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.modellk);
            this.Name = "Linhkien";
            this.Text = "Linhkien";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox modellk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tenlk;
        private System.Windows.Forms.TextBox malk;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Taikhoan;
        private System.Windows.Forms.DataGridViewTextBoxColumn Vaitro;
        private System.Windows.Forms.Button button1;
    }
}