using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Final_project.Models.DAL;


namespace Final_project.Models
{
    public class Person
    {
        string phoneNumber;
        string firstName;
        string lastName;
        string email;

        public Person() { 
        }

        public Person(string phoneNumber, string firstName, string lastName, string email)
        {
            this.PhoneNumber = phoneNumber;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
        }

        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Email { get => email; set => email = value; }

        //public int Insertper()
        //{
        //    DataServices ds = new DataServices();
        //    int status = ds.Insertper(this);
        //    return status;
        //}
    }
}