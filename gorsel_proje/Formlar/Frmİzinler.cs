using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gorsel_proje.Entity;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace gorsel_proje.Formlar
{
    public partial class Frmİzinler : Form
    {
        public Frmİzinler()
        {
            InitializeComponent();
        }
        DbistakipEntities db = new DbistakipEntities();
        void izinler()
        {
            var degerler = from x in db.Tblizinler
                           select new
                           {
                               x.ID,
                               Personel = x.TblPersonel.Ad + " " + x.TblPersonel.Soyad,
                               x.BaslangicTarih,
                               x.BitisTarih,
                               x.Aciklama
                           };
            gridControl1.DataSource = degerler.ToList();
        }

        private void Frmİzinler_Load(object sender, EventArgs e)
        {
            izinler();

            var personeller = (from x in db.TblPersonel.Where(x => x.Durum == true)
                               select new
                                {
                                    x.ID,
                                    adsoyad = x.Ad + " " + x.Soyad
                                }).ToList();

            lookUpEdit1.Properties.ValueMember = "ID";
            lookUpEdit1.Properties.DisplayMember = "adsoyad";
            lookUpEdit1.Properties.DataSource = personeller;
        }

        private void BtnListele_Click(object sender, EventArgs e)
        {
            izinler();
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            Tblizinler t = new Tblizinler();
            t.Personel = int.Parse(lookUpEdit1.EditValue.ToString());
            t.BaslangicTarih = TxtBaslangicTarih.Text;
            t.BitisTarih = TxtBitisTarih.Text;
            t.Aciklama = TxtAcıklama.Text;
            db.Tblizinler.Add(t);
            db.SaveChanges();
            XtraMessageBox.Show("Yeni İzin Kaydı Başarılı Bir Şekilde Gerçekleşti", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            izinler();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            int x = int.Parse(TxtID.Text);
            var deger = db.Tblizinler.Find(x);
            db.Tblizinler.Remove(deger);
            db.SaveChanges();
            XtraMessageBox.Show("İzin Silme İşlemi Başarılı Şekilde Gerçekleşti", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            izinler();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            int x = int.Parse(TxtID.Text);
            var deger = db.Tblizinler.Find(x);
            deger.Personel = int.Parse(lookUpEdit1.EditValue.ToString());
            deger.BaslangicTarih = TxtBaslangicTarih.Text;
            deger.BitisTarih = TxtBitisTarih.Text;
            deger.Aciklama = TxtAcıklama.Text;
            db.SaveChanges();
            XtraMessageBox.Show("İzin Bilgisi Başarılı Bir Şekilde Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            izinler();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            TxtID.Text = gridView1.GetFocusedRowCellValue("ID").ToString();
            lookUpEdit1.Text = gridView1.GetFocusedRowCellValue("Personel").ToString();
            TxtBaslangicTarih.Text = gridView1.GetFocusedRowCellValue("BaslangicTarih").ToString();
            TxtBitisTarih.Text = gridView1.GetFocusedRowCellValue("BitisTarih").ToString();
            TxtAcıklama.Text = gridView1.GetFocusedRowCellValue("Aciklama").ToString();
        }
    }
}