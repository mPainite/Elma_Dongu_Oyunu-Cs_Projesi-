using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ElmaDöngüsü
{
    public partial class AnaMenu : Form
    {
        private Button button1;
        private Button btnKurallar;
        private PictureBox CardsP;

        public AnaMenu()
        {
            InitializeComponent();

            // Butonu resmin içine yerleştiriyoruz
            button1.Parent = CardsP;
            button1.Location = new Point(105, 25); // Play ikonunun olduğu yer

            // --- ÖNEMLİ AYARLAR ---
            button1.Cursor = Cursors.Hand; // Kod ile el imlecini zorla
            button1.BringToFront();       // Butonu en üst katmana getir (tıklanabilir olması için)

            // Şeffaflık ayarları (Tekrar kontrol edelim)
            button1.BackColor = Color.Transparent;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;

            btnKurallar.Size = new Size(40, 40);

            // 2. Bir grafik yolu (GraphicsPath) oluşturuyoruz
            GraphicsPath daireYolu = new GraphicsPath();

            // 3. Butonun boyutlarında bir elips (daire) ekliyoruz
            daireYolu.AddEllipse(0, 0, btnKurallar.Width, btnKurallar.Height);

            // 4. Butonun görünen bölgesini (Region) bu daire yolu ile sınırlıyoruz
            btnKurallar.Region = new Region(daireYolu);

            // Görsel ayarlar
            btnKurallar.FlatStyle = FlatStyle.Flat;
            btnKurallar.FlatAppearance.BorderSize = 0; // Kare kenarlıkları tamamen yok eder
            btnKurallar.BackColor = Color.Gold; // İç rengini belirle
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnaMenu));
            this.CardsP = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnKurallar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.CardsP)).BeginInit();
            this.SuspendLayout();
            // 
            // CardsP
            // 
            this.CardsP.BackColor = System.Drawing.Color.Transparent;
            this.CardsP.BackgroundImage = global::ElmaDöngüsü.Properties.Resources.StartScreen;
            this.CardsP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CardsP.Location = new System.Drawing.Point(260, 40);
            this.CardsP.Name = "CardsP";
            this.CardsP.Size = new System.Drawing.Size(361, 294);
            this.CardsP.TabIndex = 0;
            this.CardsP.TabStop = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(699, 444);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(153, 117);
            this.button1.TabIndex = 0;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnKurallar
            // 
            this.btnKurallar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnKurallar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKurallar.Font = new System.Drawing.Font("Arial", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnKurallar.Location = new System.Drawing.Point(28, 479);
            this.btnKurallar.Name = "btnKurallar";
            this.btnKurallar.Size = new System.Drawing.Size(45, 45);
            this.btnKurallar.TabIndex = 1;
            this.btnKurallar.Text = "?";
            this.btnKurallar.UseVisualStyleBackColor = true;
            this.btnKurallar.Click += new System.EventHandler(this.btnKurallar_Click);
            // 
            // AnaMenu
            // 
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(854, 562);
            this.Controls.Add(this.btnKurallar);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.CardsP);
            this.DoubleBuffered = true;
            this.Name = "AnaMenu";
            ((System.ComponentModel.ISupportInitialize)(this.CardsP)).EndInit();
            this.ResumeLayout(false);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 1. OyuncuSec sınıfından yeni bir form nesnesi oluşturuyoruz
            OyuncuSec secimEkrani = new OyuncuSec();
            // 2. Mevcut Ana Menü formunu gizliyoruz
            this.Hide();
            // 3. Seçim ekranını açıyoruz (ShowDialog, o pencere kapanana kadar bu kodu bekletir)
            secimEkrani.ShowDialog();
            // 4. Seçim ekranı kapandığında (veya içinden oyun başlatılıp bittiğinde) menüyü tekrar gösteriyoruz
            this.Show();
        }

        private void btnKurallar_Click(object sender, EventArgs e)
        {
            KurallarForm kf = new KurallarForm();
            // Ana menünün ortasında açılması için:
            kf.ShowDialog();
        }
    }
}