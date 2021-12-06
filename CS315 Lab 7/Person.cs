using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS315_Lab_7
{
    class Person
    {
        private string _First;
        private string _Last;
        private string _Address;
        private string _City;
        private string _State;
        private string _Zip;
        private string _Email;
        private string _Phone;

        public Person()
        {

        }
        [Required]
       public string FirstName
        {
            set { _First = value;  }
            get { return _First; }
        }

        [Required]
        public string LastName
        {
            set { _Last = value; }
            get { return _Last; }
        }

        public string Address
        {
            set { _Address = value; }
            get { return _Address; }
        }
        public string City
        {
            set { _City = value; }
            get { return _City; }
        }
        public string State
        {
            set { _State = value; }
            get { return _State; }
        }
        public string Zip
        {
            set { _Zip = value; }
            get { return _Zip; }
        }

        [Required]
        public string Email
        {
            set { _Email = value; }
            get { return _Email; }
        }

        [Required]
        public string Phone
        {
            set { _Phone = value; }
            get { return _Phone; }
        }
    }
}
