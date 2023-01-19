using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using gorsel_proje.Entity;

namespace gorsel_proje.Formlar
{
    public partial class FrmGorevEkle : Form
    {
        public FrmGorevEkle()
        {
            InitializeComponent();
        }

        private void BtnVazgec_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        DbistakipEntities db = new DbistakipEntities();
        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            TblGorevler t = new TblGorevler();
            t.Aciklama = TxtAcıklama.Text;
            t.Durum = "1";
            t.GorevAlan = int.Parse(LupGörevAlan.EditValue.ToString());
            t.Tarih = DateTime.Parse(LupTarih.Text);
            t.GorevVeren = int.Parse(TxtGorevVeren.Text);
            db.TblGorevler.Add(t);
            db.SaveChanges();
            XtraMessageBox.Show("Görev Başarılı Bir Şekilde Tanımlandı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FrmGorevEkle_Load(object sender, EventArgs e)
        {
            var degerler = (from x in db.TblPersonel
                                select new
                                {
                                    x.ID,
                                    AdSoyad = x.Ad + " " + x.Soyad
                                }).ToList();

            LupGörevAlan.Properties.ValueMember = "ID";
            LupGörevAlan.Properties.DisplayMember = "AdSoyad";
            LupGörevAlan.Properties.DataSource = degerler;
        }
    }
}
