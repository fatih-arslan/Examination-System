
namespace Exam
{
    partial class GirisEkrani
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
            this.lblOgrSifre = new System.Windows.Forms.LinkLabel();
            this.btnOgrGiris = new System.Windows.Forms.Button();
            this.textBoxAdmSifre = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxAdmUN = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblOgrSifre
            // 
            this.lblOgrSifre.AutoSize = true;
            this.lblOgrSifre.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblOgrSifre.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblOgrSifre.Location = new System.Drawing.Point(304, 391);
            this.lblOgrSifre.Name = "lblOgrSifre";
            this.lblOgrSifre.Size = new System.Drawing.Size(147, 25);
            this.lblOgrSifre.TabIndex = 14;
            this.lblOgrSifre.TabStop = true;
            this.lblOgrSifre.Text = "Şifremi unuttum";
            // 
            // btnOgrGiris
            // 
            this.btnOgrGiris.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnOgrGiris.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOgrGiris.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnOgrGiris.ForeColor = System.Drawing.Color.White;
            this.btnOgrGiris.Location = new System.Drawing.Point(233, 463);
            this.btnOgrGiris.Name = "btnOgrGiris";
            this.btnOgrGiris.Size = new System.Drawing.Size(149, 43);
            this.btnOgrGiris.TabIndex = 4;
            this.btnOgrGiris.Text = "Giriş";
            this.btnOgrGiris.UseVisualStyleBackColor = false;
            this.btnOgrGiris.Click += new System.EventHandler(this.btnOgrGiris_Click);
            // 
            // textBoxAdmSifre
            // 
            this.textBoxAdmSifre.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBoxAdmSifre.Location = new System.Drawing.Point(246, 325);
            this.textBoxAdmSifre.Name = "textBoxAdmSifre";
            this.textBoxAdmSifre.Size = new System.Drawing.Size(240, 34);
            this.textBoxAdmSifre.TabIndex = 11;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label9.Location = new System.Drawing.Point(135, 328);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 29);
            this.label9.TabIndex = 10;
            this.label9.Text = "Şifre:";
            // 
            // textBoxAdmUN
            // 
            this.textBoxAdmUN.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBoxAdmUN.Location = new System.Drawing.Point(246, 247);
            this.textBoxAdmUN.Name = "textBoxAdmUN";
            this.textBoxAdmUN.Size = new System.Drawing.Size(240, 34);
            this.textBoxAdmUN.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label7.Location = new System.Drawing.Point(53, 250);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(151, 29);
            this.label7.TabIndex = 9;
            this.label7.Text = "Kullanıcı Adı:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(239, 130);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 39);
            this.label1.TabIndex = 15;
            this.label1.Text = "Giriş Yapın";
            // 
            // GirisEkrani
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.ClientSize = new System.Drawing.Size(617, 613);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBoxAdmSifre);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblOgrSifre);
            this.Controls.Add(this.textBoxAdmUN);
            this.Controls.Add(this.btnOgrGiris);
            this.Name = "GirisEkrani";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Giriş Ekranı";
            this.Load += new System.EventHandler(this.GirisEkrani_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnOgrGiris;
        private System.Windows.Forms.TextBox textBoxAdmSifre;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxAdmUN;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.LinkLabel lblOgrSifre;
        private System.Windows.Forms.Label label1;
    }
}