using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using gorsel_proje.Entity;

namespace gorsel_proje.Formlar
{
    public partial class FrmGorevler : Form
    {
        public FrmGorevler()
        {
            InitializeComponent();
        }

        DbistakipEntities db = new DbistakipEntities();
        private void FrmGorevler_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = (from x in db.TblGorevler
                                       select new
                                       {
                                           x.ID,
                                           x.Aciklama
                                       }).ToList();

            LblAktifGorev.Text = db.TblGorevler.Where(x => x.Durum == "1").Count().ToString();
            LblTamamlananGorev.Text = db.TblGorevler.Where(x => x.Durum == "0").Count().ToString();
            LblToplamDepartman.Text = db.TblDepartmanlar.Count().ToString();

            //chartControl1.Series["Series 1"].Points.AddPoint("İnsan Kaynakları", 26);
            //chartControl1.Series["Series 1"].Points.AddPoint("Muhasebe", 34);
            //chartControl1.Series["Series 1"].Points.AddPoint("Mutfak", 15);
            //chartControl1.Series["Series 1"].Points.AddPoint("Temizlik", 48);

            chartControl1.Series["Durum"].Points.AddPoint("Aktif Görevler", int.Parse(LblAktifGorev.Text));
            chartControl1.Series["Durum"].Points.AddPoint("Tamamlanan Görevler", int.Parse(LblTamamlananGorev.Text));
        }
    }
}
