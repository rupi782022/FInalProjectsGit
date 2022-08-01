using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Final_project.Models.DAL;

namespace Final_project.Models
{
    public class OrganizationTable
    {
        string organizationName;
        string description;
        string logo;
        string notes;
        string statusText;
        string phoneNumber;
        string firstName;
        string lastName;
        string email;
        string role;
        string specializationName;
        public OrganizationTable()
        {

        }

        public OrganizationTable(string organizationName, string description, string logo, string notes, string statusText, string phoneNumber, string firstName, string lastName, string email, string role,string specializationName)
        {
            this.OrganizationName = organizationName;
            this.Description = description;
            this.Logo = logo;
            this.Notes = notes;
            this.StatusText = statusText;
            this.PhoneNumber = phoneNumber;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.Role = role;
            this.SpecializationName = specializationName;
        }
        public string OrganizationName { get => organizationName; set => organizationName = value; }
        public string Description { get => description; set => description = value; }
        public string Logo { get => logo; set => logo = value; }
        public string Notes { get => notes; set => notes = value; }
        public string StatusText { get => statusText; set => statusText = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Email { get => email; set => email = value; }
        public string Role { get => role; set => role = value; }
        public string SpecializationName { get => specializationName; set => specializationName = value; }

        public List<OrganizationTable> Read()
        {
            DataServices ds = new DataServices();
            return ds.Readorg();
        }

            public List<OrganizationTable> ReadOrgs(int selectedsta)
        {
            DataServices ds = new DataServices();
            return ds.ReadOrgs(selectedsta);
        }

        public OrganizationTable ReadOrgByName(string orgName)
        {
            DataServices ds = new DataServices();
            return ds.ReadOrgByName(orgName);
        }
    }

   
}