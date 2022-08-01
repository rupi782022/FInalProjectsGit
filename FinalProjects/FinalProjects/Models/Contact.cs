using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Final_project.Models.DAL;

namespace Final_project.Models
{
    public class Contact
    {
        string phoneNumber;
        string role;
        bool isMajor;

        public Contact() { }

        public Contact(string phoneNumber, string role, bool isMajor)
        {
            this.PhoneNumber = phoneNumber;
            this.Role = role;
            this.IsMajor = isMajor;
        }

        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public string Role { get => role; set => role = value; }
        public bool IsMajor { get => isMajor; set => isMajor = value; }

        //public int Insertcon()
        //{
        //    DataServices ds = new DataServices();
        //    int status = ds.Insertcon(this);
        //    return status;
        //}
    }
}