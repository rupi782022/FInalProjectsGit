using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Final_project.Models.DAL;


namespace Final_project.Models
{
    public class OrgContact
    {
        string phoneNumber;
        string firstName;
        string lastName;
        string email;
        string role;
        bool isMajor;
        string organizationName;
        bool ContactStatus;

        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Email { get => email; set => email = value; }
        public string Role { get => role; set => role = value; }
        public bool IsMajor { get => isMajor; set => isMajor = value; }
        public string OrganizationName { get => organizationName; set => organizationName = value; }
        public bool ContactStatus1 { get => ContactStatus; set => ContactStatus = value; }

        public OrgContact() { }

        public OrgContact(string phoneNumber, string firstName, string lastName, string email, string role, bool isMajor, string organizationName, bool contactStatus)
        {
            this.PhoneNumber = phoneNumber;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.Role = role;
            this.IsMajor = isMajor;
            this.OrganizationName = organizationName;
            ContactStatus1 = contactStatus;
        }

        public int Insertcon()
        {
            DataServices ds = new DataServices();
            int status = ds.Insertcon(this);
            return status;
        }

        public void Update(OrgContact contact)
        {
            DataServices ds = new DataServices();
            ds.UpdateContact(contact);
        }

        public List<OrgContact> getContacts()
        {
            DataServices ds = new DataServices();
            return ds.getContacts();
        }
        public List<OrgContact> getContactsOrg(string orgName)
        {
            DataServices ds = new DataServices();
           return ds.getContactsOrg(orgName);
        }
        
        public OrgContact getContactByPhone(string phone)
        {
            DataServices ds = new DataServices();
            return ds.getContactbyPhone(phone);
        }
        public void UpdateMajor(string organizationName,string majorPhone)
        {
            DataServices ds = new DataServices();
            ds.UpdateMajor(organizationName, majorPhone);
        }

        public void UpdateConStatus(string phone,int StatusId)
        {
            DataServices ds = new DataServices();
            ds.UpdateConStatus(phone, StatusId);
        }
        public void DeleteOrgContact(string orgName)
        {
            DataServices ds = new DataServices();
            ds.UpdateConStatusByOrgName(orgName);
        }
        

    }
}