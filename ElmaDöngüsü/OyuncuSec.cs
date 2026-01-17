using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElmaDöngüsü
{
    class OyuncuSec : Form
    {
        private Button button2;
        private Label label1;
        private Label label2;
        private PictureBox pbHarita1;
        private PictureBox pbHarita5;
        private PictureBox pbHarita2;
        private PictureBox pbHarita3;
        private PictureBox pbHarita4;
        private PictureBox pbHarita6;
        private PictureBox pbHarita7;
        private PictureBox pbHarita8;
        private Button button1;

        public OyuncuSec()
        {
            InitializeComponent();

            this.BackgroundImage = Properties.Resources.PlayG;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.DoubleBuffered = true;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        // OyuncuSec.cs
        private int secilenHaritaId = 1; // Varsayılan

        private void Harita_Click(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            // İsimden ID'yi al (pbHarita5 -> 5)
            secilenHaritaId = int.Parse(pb.Name.Replace("pbHarita", ""));
            foreach (Control c in this.Controls)
                if (c is PictureBox && c.Name.StartsWith("pbHarita")) ((PictureBox)c).BorderStyle = BorderStyle.None;
            pb.BorderStyle = BorderStyle.Fixed3D;
        }

        private void OyunuBaslat(int mod)
        {
            // Form1'e hem modu hem harita ID'sini gönderiyoruz
            Form1 oyun = new Form1(mod, secilenHaritaId);
            this.Hide();
            oyun.ShowDialog();
            this.Close();
        }

        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pbHarita1 = new System.Windows.Forms.PictureBox();
            this.pbHarita5 = new System.Windows.Forms.PictureBox();
            this.pbHarita2 = new System.Windows.Forms.PictureBox();
            this.pbHarita3 = new System.Windows.Forms.PictureBox();
            this.pbHarita4 = new System.Windows.Forms.PictureBox();
            this.pbHarita6 = new System.Windows.Forms.PictureBox();
            this.pbHarita7 = new System.Windows.Forms.PictureBox();
            this.pbHarita8 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbHarita1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHarita5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHarita2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHarita3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHarita4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHarita6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHarita7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHarita8)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button1.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button1.Location = new System.Drawing.Point(16, 314);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(167, 60);
            this.button1.TabIndex = 0;
            this.button1.Text = "TEK OYUNCULU";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button2.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button2.Location = new System.Drawing.Point(199, 314);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(167, 60);
            this.button2.TabIndex = 0;
            this.button2.Text = "İKİ OYUNCULU";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Harita:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(12, 271);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Mod:";
            // 
            // pbHarita1
            // 
            this.pbHarita1.BackgroundImage = global::ElmaDöngüsü.Properties.Resources.mp1;
            this.pbHarita1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbHarita1.Location = new System.Drawing.Point(17, 56);
            this.pbHarita1.Name = "pbHarita1";
            this.pbHarita1.Size = new System.Drawing.Size(70, 70);
            this.pbHarita1.TabIndex = 2;
            this.pbHarita1.TabStop = false;
            this.pbHarita1.Click += new System.EventHandler(this.Harita_Click);
            // 
            // pbHarita5
            // 
            this.pbHarita5.BackgroundImage = global::ElmaDöngüsü.Properties.Resources.mp5;
            this.pbHarita5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbHarita5.Location = new System.Drawing.Point(17, 148);
            this.pbHarita5.Name = "pbHarita5";
            this.pbHarita5.Size = new System.Drawing.Size(70, 70);
            this.pbHarita5.TabIndex = 2;
            this.pbHarita5.TabStop = false;
            this.pbHarita5.Click += new System.EventHandler(this.Harita_Click);
            // 
            // pbHarita2
            // 
            this.pbHarita2.BackgroundImage = global::ElmaDöngüsü.Properties.Resources.mp2;
            this.pbHarita2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbHarita2.Location = new System.Drawing.Point(110, 56);
            this.pbHarita2.Name = "pbHarita2";
            this.pbHarita2.Size = new System.Drawing.Size(70, 70);
            this.pbHarita2.TabIndex = 2;
            this.pbHarita2.TabStop = false;
            this.pbHarita2.Click += new System.EventHandler(this.Harita_Click);
            // 
            // pbHarita3
            // 
            this.pbHarita3.BackgroundImage = global::ElmaDöngüsü.Properties.Resources.mp3;
            this.pbHarita3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbHarita3.Location = new System.Drawing.Point(202, 56);
            this.pbHarita3.Name = "pbHarita3";
            this.pbHarita3.Size = new System.Drawing.Size(70, 70);
            this.pbHarita3.TabIndex = 2;
            this.pbHarita3.TabStop = false;
            this.pbHarita3.Click += new System.EventHandler(this.Harita_Click);
            // 
            // pbHarita4
            // 
            this.pbHarita4.BackgroundImage = global::ElmaDöngüsü.Properties.Resources.mp4;
            this.pbHarita4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbHarita4.Location = new System.Drawing.Point(296, 56);
            this.pbHarita4.Name = "pbHarita4";
            this.pbHarita4.Size = new System.Drawing.Size(70, 70);
            this.pbHarita4.TabIndex = 2;
            this.pbHarita4.TabStop = false;
            this.pbHarita4.Click += new System.EventHandler(this.Harita_Click);
            // 
            // pbHarita6
            // 
            this.pbHarita6.BackgroundImage = global::ElmaDöngüsü.Properties.Resources.mp6;
            this.pbHarita6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbHarita6.Location = new System.Drawing.Point(110, 148);
            this.pbHarita6.Name = "pbHarita6";
            this.pbHarita6.Size = new System.Drawing.Size(70, 70);
            this.pbHarita6.TabIndex = 2;
            this.pbHarita6.TabStop = false;
            this.pbHarita6.Click += new System.EventHandler(this.Harita_Click);
            // 
            // pbHarita7
            // 
            this.pbHarita7.BackgroundImage = global::ElmaDöngüsü.Properties.Resources.mp7;
            this.pbHarita7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbHarita7.Location = new System.Drawing.Point(202, 148);
            this.pbHarita7.Name = "pbHarita7";
            this.pbHarita7.Size = new System.Drawing.Size(70, 70);
            this.pbHarita7.TabIndex = 2;
            this.pbHarita7.TabStop = false;
            this.pbHarita7.Click += new System.EventHandler(this.Harita_Click);
            // 
            // pbHarita8
            // 
            this.pbHarita8.BackgroundImage = global::ElmaDöngüsü.Properties.Resources.mp8;
            this.pbHarita8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbHarita8.Location = new System.Drawing.Point(296, 148);
            this.pbHarita8.Name = "pbHarita8";
            this.pbHarita8.Size = new System.Drawing.Size(70, 70);
            this.pbHarita8.TabIndex = 2;
            this.pbHarita8.TabStop = false;
            this.pbHarita8.Click += new System.EventHandler(this.Harita_Click);
            // 
            // OyuncuSec
            // 
            this.BackgroundImage = global::ElmaDöngüsü.Properties.Resources.PlayG;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(382, 453);
            this.Controls.Add(this.pbHarita5);
            this.Controls.Add(this.pbHarita8);
            this.Controls.Add(this.pbHarita4);
            this.Controls.Add(this.pbHarita7);
            this.Controls.Add(this.pbHarita3);
            this.Controls.Add(this.pbHarita6);
            this.Controls.Add(this.pbHarita2);
            this.Controls.Add(this.pbHarita1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.DoubleBuffered = true;
            this.Name = "OyuncuSec";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.pbHarita1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHarita5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHarita2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHarita3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHarita4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHarita6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHarita7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHarita8)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OyunuBaslat(1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OyunuBaslat(2);
        }
    }
}
