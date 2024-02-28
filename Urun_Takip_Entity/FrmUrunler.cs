using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Urun_Takip_Entity
{
    public partial class FrmUrunler : Form
    {
        public FrmUrunler()
        {
            InitializeComponent();
        }
        DbUrunlerEntities db = new DbUrunlerEntities();
        void urunListesi()
        {
            var urunler = from x in db.TblUrunler
                          select new
                          {
                              x.UrunID,
                              x.UrunAd,
                              x.Stok,
                              x.AlisFiyat,
                              x.SatisFiyat,
                              x.TblKategori.Ad
                          };
            dataGridView1.DataSource = urunler.ToList();
        }
        void Temizle()
        {
            TxtAlisFiyat.Text = "";
            TxtID.Text = "";
            TxtSatisFiyat.Text = "";
            TxtUrunStok.Text = "";
            TxtUrunAd.Text = "";
        }
        private void BtnListele_Click(object sender, EventArgs e)
        {
            //dataGridView1.DataSource = db.TblUrunler.ToList();
            urunListesi();
        }

        private void FrmUrunler_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = db.TblKategori.ToList();
            comboBox1.ValueMember = "ID";
            comboBox1.DisplayMember = "Ad";
            urunListesi();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            TblUrunler t = new TblUrunler();
            t.UrunAd = TxtUrunAd.Text;
            t.Stok =short.Parse(TxtUrunStok.Text);
            t.AlisFiyat = decimal.Parse(TxtAlisFiyat.Text);
            t.SatisFiyat = decimal.Parse(TxtSatisFiyat.Text);
            t.Kategori = int.Parse(comboBox1.SelectedValue.ToString());
            db.TblUrunler.Add(t);
            db.SaveChanges();
            MessageBox.Show("Ürün başarıyla eklendi");
            urunListesi();
            Temizle();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            TxtUrunAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            TxtUrunStok.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            TxtAlisFiyat.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            TxtSatisFiyat.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
           // comboBox1.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            if(TxtID.Text!="")
            {
                int id = int.Parse(TxtID.Text);
                var x=db.TblUrunler.Find(id);
                db.TblUrunler.Remove(x);
                db.SaveChanges();
                MessageBox.Show("Seçilen ürün silindi","Silme İşlemi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("İlk olarak ürün seçimi yapınız", "Silme İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            urunListesi();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            int id=int.Parse(TxtID.Text);
            var x = db.TblUrunler.Find(id);
            x.UrunAd = TxtUrunAd.Text;
            x.Stok = short.Parse(TxtUrunStok.Text);
            x.AlisFiyat = decimal.Parse(TxtAlisFiyat.Text);
            x.SatisFiyat = decimal.Parse(TxtSatisFiyat.Text);
            x.Kategori = int.Parse(comboBox1.SelectedValue.ToString());
            db.SaveChanges();
            MessageBox.Show("Güncelleme başarılı");
            urunListesi();
        }
    }
}
