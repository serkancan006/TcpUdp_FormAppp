using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace EntityFormApp
{
    public partial class btnogrlist : Form
    {
        DbSınavÖğrenci2Entities db = new DbSınavÖğrenci2Entities();
        public btnogrlist()
        {
            InitializeComponent();
        }



        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }



        private void btnDersList_Click(object sender, EventArgs e)
        {
            SqlConnection bağlanti = new SqlConnection(@"data source=DESKTOP-JFS933R;initial catalog=DbSınavÖğrenci2;integrated security=True");
            SqlCommand komut = new SqlCommand("select * from tblDersler", bağlanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.tblÖğrenci.ToList();
            dataGridView1.Columns[4].Visible = false;

        }

        private void btnNotLİst_Click(object sender, EventArgs e)
        {
           

            var query = from item in db.tblNotlar
                        select new
                        {
                            item.tblÖğrenci.ÖğrenciAd,
                            item.tblÖğrenci.ÖğrenciSoyad
                            ,
                            item.NotID,
                            item.DersID,
                            item.ÖğrenciID,
                            item.Sınav1,
                            item.Sınav2,
                            item.Sınav3,
                            item.Durum
                        };
            
            dataGridView1.DataSource = query.ToList();
        }

        private void btnogrekle_Click(object sender, EventArgs e)
        {
            tblÖğrenci t = new tblÖğrenci();
            t.ÖğrenciAd = txtOgrAd.Text;
            t.ÖğrenciSoyad = txtOgrSoyad.Text;
            t.ÖğrenciSehir = txtOgrŞehir.Text;
            db.tblÖğrenci.Add(t);
            db.SaveChanges();
            MessageBox.Show("Öğrenci Eklenmiştir");
            
        }

        private void btnogrGüncelle_Click(object sender, EventArgs e)
        { //adhoc
            int id;
            id = Convert.ToInt32(txtOgrID.Text);
            var x = db.tblÖğrenci.Find(id);
            x.ÖğrenciAd = txtOgrAd.Text;
            x.ÖğrenciSoyad = txtOgrSoyad.Text;
            x.ÖğrenciSehir = txtOgrŞehir.Text;
            db.SaveChanges();
            MessageBox.Show("Öğrenci Güncellenmiştir");
        }

        private void btnProsedür_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Sp_NotListesi();
        }

        private void btnbul_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.tblÖğrenci.Where(x => x.ÖğrenciAd == txtOgrAd.Text | x.ÖğrenciSoyad == txtOgrSoyad.Text).ToList();
        }

        private void txtOgrAd_TextChanged(object sender, EventArgs e)
        {
            string aranan = txtOgrAd.Text;
            var değerler = from item in db.tblÖğrenci where item.ÖğrenciAd.Contains(aranan) select item;
            dataGridView1.DataSource = değerler.ToList();
            //db.tblÖğrenci.Where(x=>x.ÖğrenciSoyad==aranan).ToList();
        }

        private void btnLinkEntity_Click(object sender, EventArgs e)
        {
            if (rdbAdaGöreSıralaAZ.Checked == true)
            {
                List<tblÖğrenci> liste = db.tblÖğrenci.OrderBy(p => p.ÖğrenciAd).ToList();
                dataGridView1.DataSource = liste;

                //var query= from item in db.tblÖğrenci.OrderBy(x => x.ÖğrenciAd) select new { item.ÖğrenciAd, item.ÖğrenciSehir };
                //dataGridView1.DataSource = query.ToList();
            }

            if (rdbAdaGöreSıralaZA.Checked == true)
            {
                List<tblÖğrenci> liste = db.tblÖğrenci.OrderByDescending(p => p.ÖğrenciAd).ToList();
                dataGridView1.DataSource = liste;
            }

            if (rdbİlk3Kayıt.Checked == true)
            {
                List<tblÖğrenci> liste = db.tblÖğrenci.OrderBy(p => p.ÖğrenciAd).Take(3).ToList();
                dataGridView1.DataSource = liste;
            }

            if (rdbIdyeGöreBul.Checked == true)
            {
                int aranan = Convert.ToInt32(txtOgrID.Text);
                List<tblÖğrenci> liste = db.tblÖğrenci.Where(p => p.ÖğrenciID == aranan).ToList();
                dataGridView1.DataSource = liste;
            }

            if (rdbAdıAileBaşlayan.Checked == true)
            {
                List<tblÖğrenci> liste = db.tblÖğrenci.Where(x => x.ÖğrenciAd.StartsWith("A")).ToList();
                dataGridView1.DataSource = liste;
            }

            if (rdbDeğerVarMı.Checked == true)
            {
                bool değer = db.tblKulüpler.Any();
                MessageBox.Show(değer.ToString(), "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (rdbToplamÖğrenci.Checked == true)
            {
                int toplam = db.tblÖğrenci.Count();
                MessageBox.Show(toplam.ToString(), "Toplam Öğrenci Saysı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (rdbNotlarınToplamı.Checked == true)
            {
                var toplam = db.tblNotlar.Sum(x => x.Sınav1);
                MessageBox.Show(toplam.ToString(), "Birinci Sınav Notları");
            }

            if (RdbSınav1Ortalama.Checked == true)
            {
                var ortalama = db.tblNotlar.Average(p => p.Sınav1);
                MessageBox.Show(ortalama.ToString());
            }

            if (rdbSınav1OrtalamadanYüksekOlanlar.Checked == true)
            {
                var ortalama = db.tblNotlar.Average(p => p.Sınav1);
                List<tblNotlar> Liste = db.tblNotlar.Where(p => p.Sınav1 > ortalama).ToList();
                dataGridView1.DataSource = Liste;
            }

            if (rdbEnDüşükSınav1.Checked == true)
            {
                var max = db.tblNotlar.Max(x => x.Sınav1);
                MessageBox.Show(max.ToString());
            }

            if (rdbEnDüşükSınav1.Checked == true)
            {
                var min = db.tblNotlar.Min(x => x.Sınav1);
                List<tblNotlar> liste = db.tblNotlar.Where(p => p.Sınav1 == min).ToList();
                dataGridView1.DataSource = liste;
            }

            if (rdb1denDüşükAlanlar.Checked == true)
            {
                var değerler = db.tblNotlar.Where(p => p.Sınav1 < 50);
                dataGridView1.DataSource = değerler.ToList();
            }

            if (rdbAdıMehmetOlan.Checked == true)
            {
                List<tblÖğrenci> liste = db.tblÖğrenci.Where(x => x.ÖğrenciAd == "mehmet").ToList();
                dataGridView1.DataSource = liste;
            }

            if (rdbAdSoyadTextdenAl.Checked == true)
            {
                var değerler = db.tblÖğrenci.Where(p => p.ÖğrenciAd == txtOgrAd.Text | p.ÖğrenciSoyad == txtOgrSoyad.Text);
                dataGridView1.DataSource = değerler.ToList();
            }

            if (rdbÖğrenciSoyadGetir.Checked == true)
            {
                var değer = db.tblÖğrenci.Select(x => new { Soyadı = x.ÖğrenciSoyad, Adı = x.ÖğrenciAd });
                //var değer = db.tblÖğrenci.Select(x => x.ÖğrenciSoyad);   bu kısım lenghtini yani harf uzunluğunu getirir
                dataGridView1.DataSource = değer.ToList();
            }

            if (rdbAdBüyükSoyadKüçükAl.Checked == true)
            {
                var değerler = db.tblÖğrenci.Select(x => new { Soyadı = x.ÖğrenciSoyad.ToLower(), Adı = x.ÖğrenciAd.ToUpper() });
                dataGridView1.DataSource = değerler.ToList();
            }

            if (rdbŞartlıSeçim.Checked == true)
            {
                var değerler = db.tblÖğrenci.Select(x => new { AD = x.ÖğrenciAd, SOYAD = x.ÖğrenciSoyad }).Where(y => y.AD != "Nazmiye").ToList();
                dataGridView1.DataSource = değerler;
            }

            if (rdbSınavıGeçtiMi.Checked == true)
            {
                var değerler = db.tblNotlar.Select(x => new
                { OgrenciAd = x.ÖğrenciID, Ortalama = x.Ortalama, Durum = x.Durum == true ? "GEÇTİ" : "KALDI" });
                dataGridView1.DataSource = değerler.ToList();
            }

            if (rdbBirleştir.Checked == true)
            {
                var değerler = db.tblNotlar.SelectMany(x => db.tblÖğrenci.Where(y => y.ÖğrenciID == x.ÖğrenciID), (x, y) =>
                new { x.ÖğrenciID, y.ÖğrenciAd, y.ÖğrenciSoyad, x.Ortalama, Durum = x.Durum == true ? "GEÇTİ" : "KALDI" });
                dataGridView1.DataSource = değerler.ToList();
            }


        }

        private void btnJoin_Click(object sender, EventArgs e)
        {
            var sorgu = from d1 in db.tblNotlar
                        join d2 in db.tblÖğrenci
                        on d1.ÖğrenciID equals d2.ÖğrenciID
                        join d3 in db.tblDersler
                        on d1.DersID equals d3.DersID
                        join d4 in db.tblKulüpler      //Kulüpler ilişkisiz tablo olmasına rağmen bu şekilde erişim yapabildik
                        on d1.DersID equals d4.KulüpID
                        select new
                        {
                            ÖĞRENCİ = d2.ÖğrenciAd + "" + d2.ÖğrenciSoyad,
                            DERS = d3.DersAd,
                            SINAV1 = d1.Sınav1,
                            SINAV2 = d1.Sınav2,
                            SINAV3 = d1.Sınav3,
                            DURUM = d1.Durum,
                            Deneme = d4.KulüpAd
                        };
            dataGridView1.DataSource = sorgu.ToList();


        }

        private void btnogrsil_Click(object sender, EventArgs e)
        {
            int id =Convert.ToInt32(txtOgrID.Text);
            var query = db.tblÖğrenci.Find(id);
            db.tblÖğrenci.Remove(query);
            db.SaveChanges();
            MessageBox.Show("Öğrenci Silinmiştir");
        }
    }
}
