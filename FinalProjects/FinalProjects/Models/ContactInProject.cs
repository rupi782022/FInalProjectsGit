using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Final_project.Models.DAL;


namespace Final_project.Models
{
    public class ContactInProject
    {
        string contactPhoneNumber;
        string projectName;
        string organizationName;
        bool contact1;


        public ContactInProject(){}

        public ContactInProject(string contactPhoneNumber, string projectName, string organizationName, bool contact1)
        {
            this.ContactPhoneNumber = contactPhoneNumber;
            this.ProjectName = projectName;
            this.OrganizationName = organizationName;
            this.Contact1 = contact1;
        }

        public string ContactPhoneNumber { get => contactPhoneNumber; set => contactPhoneNumber = value; }
        public string ProjectName { get => projectName; set => projectName = value; }
        public string OrganizationName { get => organizationName; set => organizationName = value; }
        public bool Contact1 { get => contact1; set => contact1 = value; }



        //public List<ContactInProject> Read()
        //{
        //    DataServices ds = new DataServices();
        //    return ds.Readpro();
        //}

        public int InsertPro()
        {
            DataServices ds = new DataServices();
            int status = ds.InsertConP(this);
            return status;
        }

        public void Update(ContactInProject contact)
        {
            DataServices ds = new DataServices();
            ds.UpdateProContact(contact);
        }
    }
}