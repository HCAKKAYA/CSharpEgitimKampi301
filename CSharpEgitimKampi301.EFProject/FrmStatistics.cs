using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpEgitimKampi301.EFProject
{
    public partial class FrmStatistics : Form
    {
        public FrmStatistics()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        EgitimKampiEfTravelDbEntities db = new EgitimKampiEfTravelDbEntities();
        private void FrmStatistics_Load(object sender, EventArgs e)
        {
            //Toplam Lokasyon Sayısı
            lblLocationCount.Text = db.TblLocation.Count().ToString();

            // Toplam Kapasite Sayısı
            lblSumCapacity.Text = db.TblLocation.Sum(x => x.LocationCapacity).ToString();

            //Toplam Rehber Sayısı
            lblGuideCount.Text = db.TblGuide.Count().ToString();

            //Ortalama Kapasite
            lblAvgCapacity.Text = string.Format("{0:0.00}", db.TblLocation.Average(x => x.LocationCapacity));

            //Ortalama Tur Fiyatı
            lblAvgLocationPrice.Text = string.Format("{0:0.00}  ₺", db.TblLocation.Average(x => x.LocationPrice));

            //Eklenen Son Ülke
            int lastCountryId = db.TblLocation.Max(x => x.LocationId);
            lblLastCountryName.Text  = db.TblLocation.Where(x => x.LocationId == lastCountryId).Select(y => 
            y.LocationCountry).FirstOrDefault();

            //Kapadokta Tur Kapasitesi
            lblCappadociaLocationCapacity.Text = db.TblLocation.Where(x => x.LocationCity == "Kapadokya").Select(y => 
            y.LocationCapacity).FirstOrDefault().ToString();

            //Türkiye'deki Bütün Turların Ortalama Kapasitesi
            lblTurkiyeCapacityAvg.Text = db.TblLocation.Where(x => x.LocationCountry == "Türkiye").Average(y =>
            y.LocationCapacity).ToString();

            //Roma Gezisinin Rehberi
            var romeGuideId = db.TblLocation.Where(x => x.LocationCity == "Roma Turistik").Select(y =>
            y.GuideId).FirstOrDefault();

            lblRomeGuideName.Text = db.TblGuide.Where(x => x.GuideId == romeGuideId).Select(y => y.GuideName + " " + 
            y.GuideSurname).FirstOrDefault().ToString();

            //En Yüksek Kapasiteye Sahip Tur
            var maxCapacity = db.TblLocation.Max(x => x.LocationCapacity);
            lblMaxCapacityLocation.Text = db.TblLocation.Where(x => x.LocationCapacity == maxCapacity).Select(y => 
            y.LocationCity).FirstOrDefault().ToString();

            //En Pahalı Tur
            var maxPrice = db.TblLocation.Max(x => x.LocationPrice);
            lblMaxPriceLocation.Text = db.TblLocation.Where(x => x.LocationPrice == maxPrice).Select(y =>
            y.LocationCity).FirstOrDefault().ToString();

            //Ayşegül Çınar Tur Sayısı
            var guideIdByNameAysegulCinar = db.TblGuide.Where(x => x.GuideName == "Ayşegül" && x.GuideSurname == "Çınar")
                .Select(y => y.GuideId).FirstOrDefault();
            lblAysegulCinarLocationCount.Text = db.TblLocation.Where(x => x.GuideId == guideIdByNameAysegulCinar).Count().ToString();
        }
    }
}
