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

        public Person(string first, string last, string address, string city, string state, string zip, string email, string phone)
        {
            _First = first;
            _Last = last;
            _Address = address;
            _City = city;
            _State = state;
            _Zip = zip;
            _Email = email;
            _Phone = phone;
        }
        [Required()]
       public string FirstName
        {
            set { _First = value;  }
            get { return _First; }
        }

        [Required()]
        [StringLength(20)]
        public string LastName
        {
            set { _Last = value; }
            get { return _Last; }
        }

        [Required()]
        public string Address
        {
            set { _Address = value; }
            get { return _Address; }
        }
        [Required()]
        public string City
        {
            set { _City = value; }
            get { return _City; }
        }
        [Required()]
        public string State
        {
            set { _State = value; }
            get { return _State; }
        }
        [Required()]
        [Range(1000,99999)]
        public string Zip
        {
            set { _Zip = value; }
            get { return _Zip; }
        }

        [Required()]
        [EmailAddress]
        public string Email
        {
            set { _Email = value; }
            get { return _Email; }
        }

        [Required()]
        public string Phone
        {
            set { _Phone = value; }
            get { return _Phone; }
        }
    }
}
