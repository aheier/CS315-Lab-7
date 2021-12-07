using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
                buttonValidate_Click(sender, e);
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
            //MessageBox.Show(resume);
            SetName(resume);
        }
        public void SetAddress(string resume)
        {
            Regex rx = new Regex(@"\b(\d{2,7}\s*[\w\s]+)[\.,]\s*(\w+)?[\s.,]+?(\w+)[,.\s]*(\d{5})",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);

            MatchCollection matches = rx.Matches(resume);
            if (matches.Count == 0) return;
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
            if (matches.Count == 0) return;
            GroupCollection groups = matches[0].Groups;
            //MessageBox.Show(groups[0].ToString());
            textBoxPhone.Text = $"({groups[1]})-{groups[2]}-{groups[3]}";

        }
        public void SetEmail(string resume)
        {
            Regex rx = new Regex(@"([\w.-]+@[\w.]+[\w]{2,4})",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);
            MatchCollection matches = rx.Matches(resume);
            if (matches.Count == 0) return;
            //MessageBox.Show(matches.Count.ToString());
            if (matches.Count != 0)
            {
                GroupCollection groups = matches[0].Groups;
                textBoxEmail.Text = groups[0].ToString();
            }
        }
        public void SetName(string resume)
        {
            Regex rx = new Regex(@"([a-zA-Z]{2,30})\s([a-zA-Z]+)",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);
            MatchCollection matches = rx.Matches(resume);
            if (matches.Count == 0) return;
            //MessageBox.Show(matches.Count.ToString());
            if (matches.Count != 0)
            {
                GroupCollection groups = matches[0].Groups;
                textBoxFirstName.Text = char.ToUpper(groups[1].ToString()[0]) + groups[1].ToString().ToLower().Substring(1);
                textBoxLastName.Text = char.ToUpper(groups[2].ToString()[0]) + groups[2].ToString().ToLower().Substring(1);
            }
        }

        private void buttonValidate_Click(object sender, EventArgs e)
        {
            Person person;
            errorProvider4.Clear();
            if(textBoxResume.Text == null || textBoxResume.Text == "")
            {
                errorProvider1.SetError(textBoxResume, "Please select a resume");
                return;
            }
            person = new Person(
                first: textBoxFirstName.Text.Trim(),
                last: textBoxLastName.Text.Trim(),
                address: textBoxAddress.Text.Trim(),
                city: textBoxCity.Text.Trim(),
                state: textBoxState.Text.Trim(),
                zip: textBoxZip.Text.Trim(),
                email: textBoxEmail.Text.Trim(),
                phone: textBoxPhone.Text.Trim()) ; 
            ValidateName(person);
            //MessageBox.Show((textBoxCity.Text == null || textBoxCity.Text == "")? "empty" : "not empty");
            ValidateAddress(person);
            ValidateContact(person);
            

        }
        private void ValidateName(Person person)
        {
            List<ValidationResult> firstNameResults = new List<ValidationResult>();
            ValidationContext firstNamecontext = new ValidationContext(person, null, null)
            {
                MemberName = "FirstName"
            };
            List<ValidationResult> lastNameResults = new List<ValidationResult>();
            ValidationContext lastNamecontext = new ValidationContext(person, null, null)
            {
                MemberName = "LastName"
            };
            bool isFirstNameValid = Validator.TryValidateProperty(person.FirstName, firstNamecontext, firstNameResults);
            if (!isFirstNameValid)
            {
                errorProvider1.SetError(textBoxFirstName, firstNameResults[0].ErrorMessage);
            }
            else
            {
                errorProvider1.Clear();
            }
            bool isLastNameValid = Validator.TryValidateProperty(person.LastName, lastNamecontext, lastNameResults);
            if (!isLastNameValid)
            {
                errorProvider2.SetError(textBoxLastName, lastNameResults[0].ErrorMessage);
            }
            else
            {
                errorProvider2.Clear();
            }
        }

        private void ValidateAddress(Person person)
        {
            List<ValidationResult> addressResults = new List<ValidationResult>();
            ValidationContext addresscontext = new ValidationContext(person, null, null)
            {
                MemberName = "Address"
            };
            List<ValidationResult> cityResults = new List<ValidationResult>();
            ValidationContext citycontext = new ValidationContext(person, null, null)
            {
                MemberName = "City"
            };
            List<ValidationResult> stateResults = new List<ValidationResult>();
            ValidationContext statecontext = new ValidationContext(person, null, null)
            {
                MemberName = "State"
            };
            List<ValidationResult> zipResults = new List<ValidationResult>();
            ValidationContext zipcontext = new ValidationContext(person, null, null)
            {
                MemberName = "Zip"
            };
            bool isAddressValid = Validator.TryValidateProperty(person.Address, addresscontext, addressResults);
            if (!isAddressValid)
            {
                errorProvider3.SetError(textBoxAddress, addressResults[0].ErrorMessage);
            }
            else
            {
                errorProvider3.Clear();
            }
            bool isCityValid = Validator.TryValidateProperty(person.City, citycontext, cityResults);
            if (!isCityValid)
            {
                errorProvider4.SetError(textBoxCity, cityResults[0].ErrorMessage);
            }
            else
            {
                errorProvider4.Clear();
            }
            bool isStateValid = Validator.TryValidateProperty(person.State, statecontext, stateResults);
            if (!isStateValid)
            {
                errorProvider5.SetError(textBoxState, stateResults[0].ErrorMessage);
            }
            else
            {
                errorProvider5.Clear();
            }
            bool isZipValid = Validator.TryValidateProperty(person.Zip, zipcontext, zipResults);
            if (!isZipValid)
            {
                errorProvider6.SetError(textBoxZip, zipResults[0].ErrorMessage);
            }
            else
            {
                errorProvider6.Clear();
            }
        }
        private void ValidateContact(Person person)
        {
            List<ValidationResult> emailResults = new List<ValidationResult>();
            ValidationContext emailcontext = new ValidationContext(person, null, null)
            {
                MemberName = "Email"
            };
            List<ValidationResult> phoneResults = new List<ValidationResult>();
            ValidationContext phonecontext = new ValidationContext(person, null, null)
            {
                MemberName = "Phone"
            };
            bool isEmailValid = Validator.TryValidateProperty(person.Email, emailcontext, emailResults);
            if (!isEmailValid)
            {
                errorProvider7.SetError(textBoxEmail, emailResults[0].ErrorMessage);
            }
            else
            {
                errorProvider7.Clear();
            }
            bool isPhoneValid = Validator.TryValidateProperty(person.Phone, phonecontext, phoneResults);
            if (!isPhoneValid)
            {
                errorProvider8.SetError(textBoxPhone, phoneResults[0].ErrorMessage);
            }
            else
            {
                errorProvider8.Clear();
            }
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {

            textBoxFirstName.Text = "";
            textBoxLastName.Text = "";
            textBoxAddress.Text = "";
            textBoxCity.Text = "";
            textBoxState.Text = "";
            textBoxZip.Text = "";
            textBoxEmail.Text = "";
            textBoxPhone.Text = "";
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();
            errorProvider4.Clear();
            errorProvider5.Clear();
            errorProvider6.Clear();
            errorProvider7.Clear();
            errorProvider8.Clear();
        }
    }
}
