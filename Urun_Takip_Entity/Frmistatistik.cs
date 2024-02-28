﻿using System;
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
    public partial class Frmistatistik : Form
    {
        public Frmistatistik()
        {
            InitializeComponent();
        }
        DbUrunlerEntities db = new DbUrunlerEntities();
        private void Frmistatistik_Load(object sender, EventArgs e)
        {
            DateTime bugun = DateTime.Today;
            LblMusteriSayisi.Text = db.TblMusteri.Count().ToString();
            LblKategoriSayisi.Text = db.TblKategori.Count().ToString();
            LblUrunSayisi.Text = db.TblUrunler.Count().ToString();
            LblBeyazEsya.Text = db.TblUrunler.Count(x => x.Kategori == 1).ToString();
            LblToplamStok.Text = db.TblUrunler.Sum(x => x.Stok).ToString();
            LblBugunSatisAdedi.Text = db.TblSatislar.Count(x => x.Tarih == bugun).ToString();
            LblToplamKasa.Text = db.TblSatislar.Sum(x => x.Toplam).ToString() + " ₺";
            LblBugunSatis.Text = db.TblSatislar.Where(x => x.Tarih == bugun).Sum(y => y.Toplam).ToString() + " ₺";
            LblEnYuksekFiyatliUrun.Text = (from x in db.TblUrunler
                                           orderby x.SatisFiyat descending
                                           select x.UrunAd).FirstOrDefault();
            LblEnDusukFiyatliUrun.Text = (from x in db.TblUrunler
                                          orderby x.SatisFiyat ascending
                                          select x.UrunAd).FirstOrDefault();
            LblEnFazlaStok.Text = (from x in db.TblUrunler
                                   orderby x.Stok descending
                                   select x.UrunAd).FirstOrDefault();
            LblEnAzStok.Text = (from x in db.TblUrunler
                                orderby x.Stok ascending
                                select x.UrunAd).FirstOrDefault();
        }
    }
}
