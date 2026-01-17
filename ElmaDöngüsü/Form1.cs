using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;

namespace ElmaDöngüsü
{
    public partial class Form1 : Form
    {
        // --- AYARLAR ---
        private const int SatirSayisi = 5;
        private const int SutunSayisi = 5;
        private const int HucreBoyutu = 85;

        private int oyunModu;
        private int haritaId;

        // --- DEĞİŞKENLER (Dinamik Oluşturulacaklar) ---
        private ElmaKarti[,] oyunTahtasi;
        private Button[,] butonlar;
        private List<ElmaEvresi> kartHavuzu = new List<ElmaEvresi>();

        private ElmaEvresi[] elP1 = new ElmaEvresi[3];
        private PictureBox[] kutularP1 = new PictureBox[3];
        private int puanP1 = 0;
        private Label lblPuanP1, lblIsimP1;

        private ElmaEvresi[] elP2 = new ElmaEvresi[3];
        private PictureBox[] kutularP2 = new PictureBox[3];
        private int puanP2 = 0;
        private Label lblPuanP2, lblIsimP2;

        private int siraKimde = 1;
        private Random rastgele = new Random();

        private Timer efektZamanlayici;
        private int enYuksekSkor = 0;
        private Label lblRekor; // Rekoru gösterecek etiket

        private FlowLayoutPanel pnlComboSol; // x3, x4, x5, x6 için
        private FlowLayoutPanel pnlComboSag; // x2 (çiftler) için

        private PictureBox pbEvreSiralamasi;

        public Form1(int secilenMod, int haritaId)
        {
            InitializeComponent();
            this.oyunModu = secilenMod;
            this.haritaId = haritaId;

            // --- FORM AYARLARI ---
            this.BackgroundImage = Properties.Resources.PlayG; // Arka plan resmin
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.DoubleBuffered = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(1000, 700);
            this.Text = oyunModu == 1 ? "Elma Döngüsü - Tek Oyuncu" : "Elma Döngüsü - 2 Oyuncu Kapışma";

            ArayuzuDinamikOlustur();
            ComboPanelleriniHazirla();
            TahtayiOlustur(haritaId);
            KartHavuzunuDoldur();
            ElleriDoldur();

            if (oyunModu == 2) SirayiGuncelleGorsel();
            YazilariSeffafYap();
        }
        private void ArayuzuDinamikOlustur()
        {
            lblArtisPuani = new Label { ForeColor = Color.LightGreen, Font = new Font("Segoe UI", 26, FontStyle.Bold), AutoSize = true, Visible = false, Location = new Point(450, 20), BackColor = Color.Transparent };
            this.Controls.Add(lblArtisPuani);

            efektZamanlayici = new Timer { Interval = 1000 };
            efektZamanlayici.Tick += EfektZamanlayici_Tick;

            pbEvreSiralamasi = new PictureBox();
            pbEvreSiralamasi.Location = new Point(300, 20);
            pbEvreSiralamasi.Size = new Size(370, 80);
            pbEvreSiralamasi.SizeMode = PictureBoxSizeMode.StretchImage;
            pbEvreSiralamasi.BackColor = Color.Transparent;
            pbEvreSiralamasi.BackColor = System.Drawing.Color.Transparent;
            pbEvreSiralamasi.BackgroundImage = global::ElmaDöngüsü.Properties.Resources.Cards2;
            pbEvreSiralamasi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            pbEvreSiralamasi.Visible = false;
            this.Controls.Add(pbEvreSiralamasi);

            Button btnIpuco = new Button();
            btnIpuco.Text = "i";
            btnIpuco.Font = new Font("Arial", 14, FontStyle.Bold);
            btnIpuco.Size = new Size(15, 15);
            btnIpuco.Location = new Point(700, 20);
            btnIpuco.FlatStyle = FlatStyle.Flat;
            btnIpuco.BackColor = Color.FromArgb(150, Color.Gold);
            btnIpuco.ForeColor = Color.Black;
            btnIpuco.Cursor = Cursors.Hand;
            btnIpuco.MouseEnter += (s, e) => {
                pbEvreSiralamasi.Visible = true;
                pbEvreSiralamasi.BringToFront();
            };
            btnIpuco.MouseLeave += (s, e) => {
                pbEvreSiralamasi.Visible = false;
            };

            this.Controls.Add(btnIpuco);
            lblIsimP1 = new Label { Text = "OYUNCU 1", Location = new Point(30, 30), Font = new Font("Segoe UI", 16, FontStyle.Bold), AutoSize = true, BackColor = Color.Transparent, ForeColor = Color.White };
            lblPuanP1 = new Label { Text = "0", Location = new Point(30, 70), ForeColor = Color.Yellow, Font = new Font("Segoe UI", 24, FontStyle.Bold), AutoSize = true, BackColor = Color.Transparent };
            this.Controls.Add(lblIsimP1); this.Controls.Add(lblPuanP1);

            for (int i = 0; i < 3; i++)
            {
                kutularP1[i] = new PictureBox { Size = new Size(90, 90), Location = new Point(35, 150 + (i * 110)), SizeMode = PictureBoxSizeMode.StretchImage, BackColor = Color.FromArgb(120, Color.Black), Tag = i };
                kutularP1[i].MouseDown += KartSurukleBasladi;
                this.Controls.Add(kutularP1[i]);
            }

            if (oyunModu == 2)
            {
                int sagX = 850;
                lblIsimP2 = new Label { Text = "OYUNCU 2", Location = new Point(sagX, 30), Font = new Font("Segoe UI", 16, FontStyle.Bold), AutoSize = true, BackColor = Color.Transparent, ForeColor = Color.White };
                lblPuanP2 = new Label { Text = "0", Location = new Point(sagX, 70), ForeColor = Color.Yellow, Font = new Font("Segoe UI", 24, FontStyle.Bold), AutoSize = true, BackColor = Color.Transparent };
                this.Controls.Add(lblIsimP2); this.Controls.Add(lblPuanP2);

                for (int i = 0; i < 3; i++)
                {
                    kutularP2[i] = new PictureBox { Size = new Size(90, 90), Location = new Point(sagX, 150 + (i * 110)), SizeMode = PictureBoxSizeMode.StretchImage, BackColor = Color.FromArgb(120, Color.Black), Tag = i + 10 };
                    kutularP2[i].MouseDown += KartSurukleBasladi;
                    this.Controls.Add(kutularP2[i]);
                }
            }
            if (oyunModu == 1) 
            {
                lblRekor = new Label
                {
                    Text = "Rekor: 0",
                    Location = new Point(840, 30),
                    Font = new Font("Palatino Linotype", 17, FontStyle.Bold),
                    AutoSize = true,
                    ForeColor = Color.Gold,
                    BackColor = Color.Transparent
                };
                this.Controls.Add(lblRekor);
            }

            if (oyunModu == 1)
            {
                RekorYukle(haritaId);
                lblRekor.Text = "Rekor: " + enYuksekSkor;
            }


        }

        private void ComboPanelleriniHazirla()
        {
            pnlComboSol = new FlowLayoutPanel
            {
                Location = new Point(250, 60),
                Size = new Size(300, 50),
                BackColor = Color.Transparent,
                FlowDirection = FlowDirection.LeftToRight
            };

            // Sağ Üst Panel (Çiftler için)
            pnlComboSag = new FlowLayoutPanel
            {
                Location = new Point(550, 60),
                Size = new Size(200, 50),
                BackColor = Color.Transparent,
                FlowDirection = FlowDirection.RightToLeft
            };

            this.Controls.Add(pnlComboSol);
            this.Controls.Add(pnlComboSag);
        }

        private void ComboYazisiEkle(string metin, Color renk, bool solMu)
        {
            Label lbl = new Label
            {
                Text = metin,
                ForeColor = renk,
                Font = new Font("Impact", 24, FontStyle.Italic),
                AutoSize = true,
                BackColor = Color.Transparent,
                Margin = new Padding(5, 0, 5, 0)
            };

            if (solMu) pnlComboSol.Controls.Add(lbl);
            else pnlComboSag.Controls.Add(lbl);
        }
        private void YazilariSeffafYap()
        {
            foreach (Control control in this.Controls)
            {
                if (control is Label) control.BackColor = Color.Transparent;
            }
        }

        private string HaritaVerisiGetir(int id)
        {
            switch (id)
            {
                case 1: return "1111111111111111111111111";
                case 2: return "1111101010111110101011111";   //1111101010111110101011111
                case 3: return "1111110001100011000111111";   //1111110001100011000111111
                case 4: return "1101111011000001101111011";   //1101111011000001101111011
                case 5: return "1111110000111110000111111";
                case 6: return "0010001110111111111101110";
                case 7: return "0010001110111110010000100";
                case 8: return "1011110100111110010111101";   //1011110100111110010111101
                default: return "1111111111111111111111111";
            }
        }

        private void TahtayiOlustur(int haritaId)
        {
            string haritaDizisi = HaritaVerisiGetir(haritaId);
            int hucreIndex = 0;

            oyunTahtasi = new ElmaKarti[SatirSayisi, SutunSayisi];
            butonlar = new Button[SatirSayisi, SutunSayisi];

            int baslangicX = (this.ClientSize.Width - (SutunSayisi * 90)) / 2;
            int baslangicY = 120;

            for (int i = 0; i < SatirSayisi; i++)
            {
                for (int j = 0; j < SutunSayisi; j++)
                {
                    if (haritaDizisi[hucreIndex] == '1')
                    {
                        Button btn = new Button { Width = 85, Height = 85, Left = baslangicX + (j * 90), Top = baslangicY + (i * 90), Tag = new Point(i, j), AllowDrop = true, BackColor = Color.FromArgb(220, Color.WhiteSmoke), FlatStyle = FlatStyle.Flat, BackgroundImageLayout = ImageLayout.Stretch };
                        btn.FlatAppearance.BorderSize = 1;
                        btn.DragEnter += SuruklemeGirdi;
                        btn.DragDrop += KartBirakildi;
                        this.Controls.Add(btn);
                        butonlar[i, j] = btn;
                    }
                    else butonlar[i, j] = null;
                    hucreIndex++;
                }
            }
        }

        private void KartHavuzunuDoldur()
        {
            kartHavuzu.Clear();
            for (int i = 1; i <= 6; i++) for (int k = 0; k < 5; k++) kartHavuzu.Add((ElmaEvresi)i);
            kartHavuzu.Add((ElmaEvresi)rastgele.Next(1, 7));
            int n = kartHavuzu.Count;
            while (n > 1) { n--; int k = rastgele.Next(n + 1); var v = kartHavuzu[k]; kartHavuzu[k] = kartHavuzu[n]; kartHavuzu[n] = v; }
        }

        private ElmaEvresi HavuzdanKartCek() { if (kartHavuzu.Count == 0) KartHavuzunuDoldur(); var c = kartHavuzu[0]; kartHavuzu.RemoveAt(0); return c; }

        private void ElleriDoldur()
        {
            for (int i = 0; i < 3; i++)
            {
                if (elP1[i] == 0) { elP1[i] = HavuzdanKartCek(); kutularP1[i].Image = ResimGetirHelper(elP1[i]); }
                if (oyunModu == 2 && elP2[i] == 0) { elP2[i] = HavuzdanKartCek(); kutularP2[i].Image = ResimGetirHelper(elP2[i]); }
            }
        }

        private Image ResimGetirHelper(ElmaEvresi e)
        {
            switch (e)
            {
                case ElmaEvresi.Cicek: return Properties.Resources.img_1_cicek;
                case ElmaEvresi.Ham: return Properties.Resources.img_2_ham;
                case ElmaEvresi.Olgun: return Properties.Resources.img_3_olgun;
                case ElmaEvresi.Isirilmis: return Properties.Resources.img_4_isirilmis;
                case ElmaEvresi.Kocan: return Properties.Resources.img_5_kocan;
                case ElmaEvresi.Curuk: return Properties.Resources.img_6_curuk;
                default: return null;
            }
        }

        private void KartSurukleBasladi(object s, MouseEventArgs e)
        {
            var pb = (PictureBox)s; int tid = (int)pb.Tag;
            if (oyunModu == 2) { if (siraKimde == 1 && tid >= 10) return; if (siraKimde == 2 && tid < 10) return; }
            var evre = (tid < 10) ? elP1[tid] : elP2[tid - 10];
            if (evre != 0) pb.DoDragDrop(tid, DragDropEffects.Copy);
        }

        private void SuruklemeGirdi(object s, DragEventArgs e) => e.Effect = e.Data.GetDataPresent(typeof(int)) ? DragDropEffects.Copy : DragDropEffects.None;

        private void KartBirakildi(object s, DragEventArgs e)
        {
            var btn = (Button)s; Point p = (Point)btn.Tag; int tid = (int)e.Data.GetData(typeof(int));
            var evre = (tid < 10) ? elP1[tid] : elP2[tid - 10];
            if (oyunTahtasi[p.X, p.Y] == null)
            {
                oyunTahtasi[p.X, p.Y] = new ElmaKarti(p.X, p.Y, evre);
                btn.BackgroundImage = oyunTahtasi[p.X, p.Y].GorselGetir();
                PuanHesapla(p.X, p.Y);
                if (tid < 10) { elP1[tid] = 0; kutularP1[tid].Image = null; } else { elP2[tid - 10] = 0; kutularP2[tid - 10].Image = null; }
                ElleriDoldur();
                if (oyunModu == 2) { siraKimde = (siraKimde == 1) ? 2 : 1; SirayiGuncelleGorsel(); }
                OyunBittiKontrolu();
            }
        }

        private void PuanHesapla(int x, int y)
        {
            int artis = 0; var mk = oyunTahtasi[x, y]; List<Point> ikiz = new List<Point>(), zincir = new List<Point>();
            int[] dx = { 0, 0, 1, -1 }, dy = { 1, -1, 0, 0 };
            for (int i = 0; i < 4; i++)
            {
                int nx = x + dx[i], ny = y + dy[i];
                if (nx >= 0 && nx < SatirSayisi && ny >= 0 && ny < SutunSayisi && oyunTahtasi[nx, ny] != null && oyunTahtasi[nx, ny].Evre == mk.Evre) { artis += 4; ikiz.Add(new Point(nx, ny)); }
            }
            if (ikiz.Count > 0) ikiz.Add(new Point(x, y));
            ZincirBul(x, y, -1, ref zincir); ZincirBul(x, y, 1, ref zincir);
            if (zincir.Count + 1 >= 3) { artis += (zincir.Count + 2) * 2; zincir.Add(new Point(x, y)); } else zincir.Clear();
            if (artis > 0)
            {
                if (oyunModu == 1) { puanP1 += artis; lblPuanP1.Text = "Puan: " + puanP1; }
                else { if (siraKimde == 1) { puanP1 += artis; lblPuanP1.Text = puanP1.ToString(); } else { puanP2 += artis; lblPuanP2.Text = puanP2.ToString(); } }
                lblArtisPuani.Text = "+" + artis; lblArtisPuani.Visible = true;
                foreach (var pt in ikiz) butonlar[pt.X, pt.Y].BackColor = Color.LightGreen;
                foreach (var pt in zincir) butonlar[pt.X, pt.Y].BackColor = Color.LightYellow;
                efektZamanlayici.Start();
            }
            if (ikiz.Count > 0)
            {
                ComboYazisiEkle("x2", Color.DeepSkyBlue, false);
            }
            int zincirSayisi = zincir.Count;
            if (zincirSayisi >= 3)
            {
                Color comboRenk = Color.White;
                switch (zincirSayisi)
                {
                    case 3: comboRenk = Color.LightGreen; break;
                    case 4: comboRenk = Color.MediumPurple; break;
                    case 5: comboRenk = Color.Crimson; break;
                    case 6: comboRenk = Color.Gold; break;
                    default: comboRenk = Color.Yellow; break;
                }
                ComboYazisiEkle("x" + zincirSayisi, comboRenk, true);
            }
        }

        private void ZincirBul(int x, int y, int d, ref List<Point> l)
        {
            int aranan = (int)oyunTahtasi[x, y].Evre + d; if (aranan < 1 || aranan > 6) return;
            int[] dx = { 0, 0, 1, -1 }, dy = { 1, -1, 0, 0 };
            for (int i = 0; i < 4; i++)
            {
                int nx = x + dx[i], ny = y + dy[i];
                if (nx >= 0 && nx < SatirSayisi && ny >= 0 && ny < SutunSayisi && oyunTahtasi[nx, ny] != null && (int)oyunTahtasi[nx, ny].Evre == aranan) { l.Add(new Point(nx, ny)); ZincirBul(nx, ny, d, ref l); }
            }
        }

        private void EfektZamanlayici_Tick(object s, EventArgs e)
        {
            lblArtisPuani.Visible = false;
            for (int i = 0; i < SatirSayisi; i++)
                for (int j = 0; j < SutunSayisi; j++)
                    if (butonlar[i, j] != null) butonlar[i, j].BackColor = Color.FromArgb(220, Color.WhiteSmoke);

            pnlComboSol.Controls.Clear();
            pnlComboSag.Controls.Clear();
            efektZamanlayici.Stop();
        }

        private void SirayiGuncelleGorsel() { lblIsimP1.ForeColor = (siraKimde == 1) ? Color.LightGreen : Color.Gray; lblIsimP2.ForeColor = (siraKimde == 2) ? Color.LightGreen : Color.Gray; }

        private void OyunBittiKontrolu()
        {
            bool bosYerVar = false;
            for (int i = 0; i < SatirSayisi; i++)
                for (int j = 0; j < SutunSayisi; j++)
                    if (butonlar[i, j] != null && oyunTahtasi[i, j] == null) { bosYerVar = true; break; }

            if (!bosYerVar)
            {
                if (oyunModu == 1 && puanP1 > enYuksekSkor)
                {
                    enYuksekSkor = puanP1;
                    RekorKaydet(this.haritaId, enYuksekSkor);
                    MessageBox.Show($"TEBRİKLER! YENİ REKOR: {enYuksekSkor}", "Rekor Kırıldı!");
                }

                string m = (oyunModu == 1) ? $"Puan: {puanP1}" : (puanP1 > puanP2 ? "P1 KAZANDI!" : (puanP2 > puanP1 ? "P2 KAZANDI!" : "BERABERE!"));
                MessageBox.Show(m, "Oyun Bitti");
                this.Close();
            }
        }
        private void RekorYukle(int haritaId)
        {
            string dosyaYolu = $"rekor_harita_{haritaId}.txt";
            if (File.Exists(dosyaYolu))
            {
                string icerik = File.ReadAllText(dosyaYolu);
                int.TryParse(icerik, out enYuksekSkor);
            }
            else
            {
                enYuksekSkor = 0;
            }
        }

        private void RekorKaydet(int haritaId, int yeniSkor)
        {
            string dosyaYolu = $"rekor_harita_{haritaId}.txt";
            File.WriteAllText(dosyaYolu, yeniSkor.ToString());
        }
    }
}