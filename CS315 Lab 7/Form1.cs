using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CS315_Lab_7
{
    public partial class Form1 : Form
    {
        public string resumeText;
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            string filePath;
            OpenFileDialog dialog = new OpenFileDialog();
            //dialog. = "c:\\";
            //dialog.Filter = "All files (*.*)|*.*";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                filePath = dialog.FileName;
                //MessageBox.Show(filePath);
                textBoxResume.Text = filePath;
                resumeText = ExtractTextFromPdf(filePath);
                FillForm(resumeText);
            }
        }

        public static string ExtractTextFromPdf(string path)
        {
            PdfReader reader = new PdfReader(path);
            StringBuilder text = new StringBuilder();

            for (int i = 1; i <= reader.NumberOfPages; i++)
            {
                text.Append(PdfTextExtractor.GetTextFromPage(reader, i));
            }
            return text.ToString();
        }
        public void FillForm(string resume)
        {
            SetAddress(resume);
            SetPhone(resume);
            SetEmail(resume);
            MessageBox.Show(resume);
            SetName(resume);
        }
        public void SetAddress(string resume)
        {
            Regex rx = new Regex(@"\b(\d{2,7}\s*[\w\s]+[\.,])\s*(\w+[\s.,]+?)?(\w+[,.\s]*?)(\d{5})",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);

            MatchCollection matches = rx.Matches(resume);
            GroupCollection groups = matches[0].Groups;
            textBoxAddress.Text = groups[1].ToString().Replace(',',' ').Trim() ;
            textBoxCity.Text = groups[2].ToString().Replace(',', ' ').Trim();
            textBoxState.Text = groups[3].ToString();
            textBoxZip.Text = groups[4].ToString();
        }

        public void SetPhone(string resume)
        {
            Regex rx = new Regex(@"(\d{3})[\s.\-\\)]*(\d{3})[\s.-](\d{4})",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);
            MatchCollection matches = rx.Matches(resume);
            GroupCollection groups = matches[0].Groups;
            MessageBox.Show(groups[0].ToString());
            textBoxPhone.Text = $"{groups[1]}-{groups[2]}-{groups[3]}";

        }
        public void SetEmail(string resume)
        {
            Regex rx = new Regex(@"([\w.-]+@[\w.]+[\w]{2,4})",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);
            MatchCollection matches = rx.Matches(resume);
            MessageBox.Show(matches.Count.ToString());
            if (matches.Count != 0)
            {
                GroupCollection groups = matches[0].Groups;
                textBoxEmail.Text = groups[0].ToString();
            }
        }
        public void SetName(string resume)
        {
            Regex rx = new Regex(@"([a-zA-Z]+)\s([a-zA-Z]+)",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);
            MatchCollection matches = rx.Matches(resume);
            MessageBox.Show(matches.Count.ToString());
            if (matches.Count != 0)
            {
                GroupCollection groups = matches[0].Groups;
                textBoxFirstName.Text = groups[1].ToString();
                textBoxLastName.Text = groups[2].ToString();
            }
        }
    }
}
