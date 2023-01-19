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
    public partial class FrmAdmin : Form
    {
        public FrmAdmin()
        {
            InitializeComponent();
        }
        DbistakipEntities db = new DbistakipEntities();
        void Admin()
        {
            var degerler = (from x in db.TblAdmin
                            select new
                            {
                                x.ID,
                                x.Kullanici,
                                x.Sifre
                            }).ToList();
            gridControl1.DataSource = degerler;
        }

        private void BtnListele_Click(object sender, EventArgs e)
        {
            Admin();
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            TblAdmin t = new TblAdmin();
            t.Kullanici = TxtKullanici.Text;
            t.Sifre = TxtSifre.Text;
            db.TblAdmin.Add(t);
            db.SaveChanges();
            XtraMessageBox.Show("Admin Başarılı Şekilde Sisteme Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Admin();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            int x = int.Parse(TxtID.Text);
            var deger = db.TblAdmin.Find(x);
            db.TblAdmin.Remove(deger);
            db.SaveChanges();
            XtraMessageBox.Show("Admşn Silme İşlemi Başarılı Şekilde Gerçekleşti", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Admin();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            int x = int.Parse(TxtID.Text);
            var deger = db.TblAdmin.Find(x);
            deger.Kullanici = TxtKullanici.Text;
            deger.Sifre = TxtSifre.Text;
            db.SaveChanges();
            XtraMessageBox.Show("Admin Güncelleme İşlemi Başarılı Şekilde Gerçekleşti", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Admin();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            TxtID.Text = gridView1.GetFocusedRowCellValue("ID").ToString();
            TxtKullanici.Text = gridView1.GetFocusedRowCellValue("Kullanici").ToString();
            TxtSifre.Text = gridView1.GetFocusedRowCellValue("Sifre").ToString();
        }
    }
}
