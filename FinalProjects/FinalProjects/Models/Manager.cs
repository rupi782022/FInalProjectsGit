using Final_project.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_project.Models
{
    public class Manager
    {
        string mail;
        string phoneNumber;
        string firstName;
        string lastName;
        string password;

        public Manager()
        {

        }
        public Manager(string mail, string password)
        {
            this.Mail = mail;
            this.Password = password;
        }
        public Manager(string mail, string phoneNumber, string firstName, string lastName, string password)
        {
            this.Mail = mail;
            this.PhoneNumber = phoneNumber;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Password = password;
        }

        public string Mail { get => mail; set => mail = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Password { get => password; set => password = value; }

        
                public string LoginManager()
        {
            DataServices ds = new DataServices();
            return ds.LoginManager(this);
        }
    }
}