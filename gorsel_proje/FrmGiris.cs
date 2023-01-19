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

namespace gorsel_proje
{
    public partial class FrmGiris : Form
    {
        public FrmGiris()
        {
            InitializeComponent();
        }
        DbistakipEntities db = new DbistakipEntities();

        private void BtnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public bool Login(string username,string password)
        {
            var t = (from x in db.TblAdmin
                     select new
                     {
                         x.Kullanici,
                         x.Sifre
                     }).FirstOrDefault();

            if (t != null)
                return true;

            return false;
        }

        private void BtnGiris_Click(object sender, EventArgs e)
        {
            var username = TxtKullanici.Text;
            var password = TxtSifre.Text;
            var errorMessage = "";
            var isError = false;
            if(string.IsNullOrEmpty(username))
            {
                errorMessage += "Kullanıcı Adı Boş Geçilemez!\r\n";
                isError = true;
            }
            if (string.IsNullOrEmpty(password))
            {
                errorMessage += "Şifre Boş Geçilemez!\r\n";
                isError = true;
            }
            if(isError)
            {
                XtraMessageBox.Show(errorMessage, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var loginstate = Login(username,password);

            if (loginstate)
            {
                XtraMessageBox.Show("Giriş Başarılı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Form1 fr = new Form1();
                this.Hide();
                fr.Show();
            }

            else

                XtraMessageBox.Show("Giriş Başarısız","Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
