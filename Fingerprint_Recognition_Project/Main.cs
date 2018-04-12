//ISAAC D. ARCILLA (isaacdarcilla@gmail.com)

using System;
using System.Drawing;
using System.Windows.Forms;

using PatternRecognition.FingerprintRecognition.Core;
using PatternRecognition.FingerprintRecognition.FeatureExtractors;
using PatternRecognition.FingerprintRecognition.Matchers;

using MetroFramework.Forms;

namespace Fingerprint_Recognition_Project
{
    public partial class Form1 : MetroForm
    {
     
        public Form1()
        {
            InitializeComponent();
        }

        public string score;
        public string qry;
        public string temp;

        private Bitmap Change_Resolution(string file)
        {
      
                using (Bitmap bitmap = (Bitmap)Image.FromFile(file))
                {
                    using (Bitmap newBitmap = new Bitmap(bitmap))
                    {
                        newBitmap.SetResolution(500, 500);
                        return newBitmap;
                    }
                }
   
        }



        private void match(string query, string template)
        {
            Change_Resolution(query);
            Change_Resolution(template);
            // Loading fingerprints
            var fingerprintImg1 = ImageLoader.LoadImage(query);
            var fingerprintImg2 = ImageLoader.LoadImage(template);
            //// Building feature extractor and extracting features
            var featExtractor = new PNFeatureExtractor() { MtiaExtractor = new Ratha1995MinutiaeExtractor() };
            var features1 = featExtractor.ExtractFeatures(fingerprintImg1);
            var features2 = featExtractor.ExtractFeatures(fingerprintImg2);

            // Building matcher and matching
            var matcher = new PN();
            double similarity = matcher.Match(features1, features2);
            score = similarity.ToString("0.00");
            string per = "percent";
            label1.Text = similarity.ToString("0.00");
            label3.Visible = true;
        

         

        }


        private void Form1_Load(object sender, EventArgs e)
        {
   

        }




        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.ShowDialog();

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string fileName;
                fileName = dlg.FileName;
                qry = fileName;
                pictureBox1.ImageLocation = qry;
          
            }
            if (pictureBox1 != null)
            {
                button3.Enabled = true;
            }
            else {
                button3.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.ShowDialog();

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string fileName;
                fileName = dlg.FileName;
                temp= fileName;
                pictureBox2.ImageLocation = temp;
            }
            if (pictureBox2 != null)
            {
                button3.Enabled = true;
            }
            else
            {
                button3.Enabled = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            match(qry, temp);
        }

        private void label2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/isaacdarcilla");
        }
    }
}
