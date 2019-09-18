using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebBot
{
    public partial class Form1 : Form
    {
        HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
        WebClient client = new WebClient();
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Controls.Clear();
            client.Encoding = Encoding.UTF8;
            client.d
            string html = client.DownloadString(txtBoxUrl.Text);
            document.LoadHtml(html);
            //*[@id="contentProDetail"]/div/div[3]/div[2]/div[1]/div/h2
            string urunAd = document.DocumentNode.SelectSingleNode("//*[@id=\"contentProDetail\"]/div/div[3]/div[2]/div[1]/div/h1").InnerText.Trim();
            string urunAltBaslık = document.DocumentNode.SelectSingleNode("//*[@id=\"contentProDetail\"]/div/div[3]/div[2]/div[1]/div/h2").InnerText.Trim();
            var aciklama= document.DocumentNode.SelectNodes("//*[@id=\"tabPanelProDetail\"]/div/section[3]/div");
            string urunFiyat = document.DocumentNode.SelectSingleNode("//*[@id=\"contentProDetail\"]/div/div[3]/div[2]/div[3]/div[2]/div/div[1]/div/ins").InnerText.Trim();
            var resimler = document.DocumentNode.SelectNodes("//*[@id=\"contentProDetail\"]/div/div[3]/div[1]/figure/div[2]/ul/li");
            var kategoriKirilim = document.DocumentNode.SelectNodes("//*[@id=\"breadCrumb\"]/ul/li");
            var varyantlar = document.DocumentNode.SelectNodes("//*[@id=\"autoRecBox\"]/div/div");
            //*[@id="adSlider"]/div
            string kirilim = "";
            foreach (var item in kategoriKirilim)
            {
                kirilim += item.InnerText.Trim().Replace("amp;", "&")+">";
            }
            foreach (var item in resimler)
            {
                PictureBox p = new PictureBox();
                p.SizeMode = PictureBoxSizeMode.StretchImage;
                p.Click += P_Click;
                p.ImageLocation= item.Attributes["data-full"].Value;
               
                tableLayoutPanel1.Controls.Add(p);
                
            }
            labelKirilim.Text = kirilim;
            labelUrunAdi.Text = urunAd;
            labelAltBaslik.Text = urunAltBaslık;
            labelFiyat.Text = urunFiyat;
           webBrowser1.DocumentText=aciklama[0].InnerHtml;
            // GetDatas(txtBoxUrl.Text);
        }//*[@id="tabPanelProDetail"]/div/section[3]/div

        private void P_Click(object sender, EventArgs e)
        {
            using (Form form = new Form())
            {

                form.StartPosition = FormStartPosition.CenterScreen;

                PictureBox pb = new PictureBox();

                pb.Dock = DockStyle.Fill;
                pb.Image = ((PictureBox)sender).Image;
                pb.SizeMode = PictureBoxSizeMode.AutoSize;
                form.Size = pb.Size;
                form.Controls.Add(pb);
                form.ShowDialog();
            }
            sender.ToString();
            e.ToString();
        }


      
    }
}
