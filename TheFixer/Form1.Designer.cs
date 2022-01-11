
namespace TheFixer
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
            this.dugmeZatvoriPrijavu = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.korisnickoIme = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lozinka = new System.Windows.Forms.TextBox();
            this.labGreska = new System.Windows.Forms.Label();
            this.dugmePrijava = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dugmeZatvoriPrijavu
            // 
            this.dugmeZatvoriPrijavu.FlatAppearance.BorderSize = 0;
            this.dugmeZatvoriPrijavu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dugmeZatvoriPrijavu.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dugmeZatvoriPrijavu.ForeColor = System.Drawing.Color.MistyRose;
            this.dugmeZatvoriPrijavu.Location = new System.Drawing.Point(399, 0);
            this.dugmeZatvoriPrijavu.Name = "dugmeZatvoriPrijavu";
            this.dugmeZatvoriPrijavu.Size = new System.Drawing.Size(50, 50);
            this.dugmeZatvoriPrijavu.TabIndex = 0;
            this.dugmeZatvoriPrijavu.Text = "X";
            this.dugmeZatvoriPrijavu.UseVisualStyleBackColor = true;
            this.dugmeZatvoriPrijavu.Click += new System.EventHandler(this.dugmeZatvoriPrijavu_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Castellar", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.MistyRose;
            this.label1.Location = new System.Drawing.Point(100, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(259, 48);
            this.label1.TabIndex = 1;
            this.label1.Text = "THE FIXER";
            // 
            // korisnickoIme
            // 
            this.korisnickoIme.BackColor = System.Drawing.Color.ForestGreen;
            this.korisnickoIme.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.korisnickoIme.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.korisnickoIme.ForeColor = System.Drawing.Color.MistyRose;
            this.korisnickoIme.Location = new System.Drawing.Point(54, 142);
            this.korisnickoIme.Name = "korisnickoIme";
            this.korisnickoIme.Size = new System.Drawing.Size(343, 25);
            this.korisnickoIme.TabIndex = 2;
            this.korisnickoIme.Text = "Unesite korisnicko ime";
            this.korisnickoIme.TextChanged += new System.EventHandler(this.korisnickoIme_TextChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.MistyRose;
            this.panel1.Location = new System.Drawing.Point(53, 172);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(343, 1);
            this.panel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.MistyRose;
            this.panel2.Location = new System.Drawing.Point(53, 258);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(343, 1);
            this.panel2.TabIndex = 4;
            // 
            // lozinka
            // 
            this.lozinka.BackColor = System.Drawing.Color.ForestGreen;
            this.lozinka.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lozinka.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lozinka.ForeColor = System.Drawing.Color.MistyRose;
            this.lozinka.Location = new System.Drawing.Point(54, 227);
            this.lozinka.Name = "lozinka";
            this.lozinka.Size = new System.Drawing.Size(343, 25);
            this.lozinka.TabIndex = 5;
            this.lozinka.Text = "Unesite lozinku";
            this.lozinka.TextChanged += new System.EventHandler(this.lozinka_TextChanged);
            // 
            // labGreska
            // 
            this.labGreska.AutoSize = true;
            this.labGreska.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labGreska.ForeColor = System.Drawing.Color.DarkRed;
            this.labGreska.Location = new System.Drawing.Point(69, 287);
            this.labGreska.Name = "labGreska";
            this.labGreska.Size = new System.Drawing.Size(314, 24);
            this.labGreska.TabIndex = 6;
            this.labGreska.Text = "Pogresno korisnicko ime i/ili lozinka!";
            this.labGreska.Visible = false;
            // 
            // dugmePrijava
            // 
            this.dugmePrijava.BackColor = System.Drawing.Color.MistyRose;
            this.dugmePrijava.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dugmePrijava.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dugmePrijava.ForeColor = System.Drawing.Color.ForestGreen;
            this.dugmePrijava.Location = new System.Drawing.Point(100, 344);
            this.dugmePrijava.Name = "dugmePrijava";
            this.dugmePrijava.Size = new System.Drawing.Size(261, 50);
            this.dugmePrijava.TabIndex = 7;
            this.dugmePrijava.Text = "Prijavi se";
            this.dugmePrijava.UseVisualStyleBackColor = false;
            this.dugmePrijava.Click += new System.EventHandler(this.dugmePrijava_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.ForestGreen;
            this.ClientSize = new System.Drawing.Size(450, 450);
            this.Controls.Add(this.dugmePrijava);
            this.Controls.Add(this.labGreska);
            this.Controls.Add(this.lozinka);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.korisnickoIme);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dugmeZatvoriPrijavu);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button dugmeZatvoriPrijavu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox korisnickoIme;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox lozinka;
        private System.Windows.Forms.Label labGreska;
        private System.Windows.Forms.Button dugmePrijava;
    }
}

