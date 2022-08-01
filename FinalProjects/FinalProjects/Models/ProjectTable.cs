using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Final_project.Models.DAL;


namespace Final_project.Models
{
    public class ProjectTable
    {
        string projectName;
        string projectDescription;
        string url;
        string notes;
        string statusText;
        int statusId;
        int specializationId;
        string specializationName;
        string organizationName;
        string phoneNumber;
        string firstName;
        string lastName;
        string email;
        string role;
        bool contact1;

        public string ProjectName { get => projectName; set => projectName = value; }
        public string ProjectDescription { get => projectDescription; set => projectDescription = value; }
        public string Url { get => url; set => url = value; }
        public string Notes { get => notes; set => notes = value; }
        public string StatusText { get => statusText; set => statusText = value; }
        public int StatusId { get => statusId; set => statusId = value; }
        public int SpecializationId { get => specializationId; set => specializationId = value; }
        public string SpecializationName { get => specializationName; set => specializationName = value; }
        public string OrganizationName { get => organizationName; set => organizationName = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Email { get => email; set => email = value; }
        public string Role { get => role; set => role = value; }
        public bool Contact1 { get => contact1; set => contact1 = value; }

        public ProjectTable() { }

        public ProjectTable(string projectName, string projectDescription, string url, string notes, string statusText, int statusId, int specializationId, string specializationName, string organizationName, string phoneNumber, string firstName, string lastName, string email, string role, bool contact1)
        {
            this.ProjectName = projectName;
            this.ProjectDescription = projectDescription;
            this.Url = url;
            this.Notes = notes;
            this.StatusText = statusText;
            this.StatusId = statusId;
            this.SpecializationId = specializationId;
            this.SpecializationName = specializationName;
            this.OrganizationName = organizationName;
            this.PhoneNumber = phoneNumber;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.Role = role;
            this.Contact1 = contact1;
        }

        public List<ProjectTable> Read()
        {
            DataServices ds = new DataServices();
            return ds.Readpro();
        }

        public List<ProjectTable> ReadProByName(string ProName)
        {
            DataServices ds = new DataServices();
            return ds.ReadProByName(ProName);
        }

        public List<ProjectTable> ReadProjByOrg(string orgName)
        {
            DataServices ds = new DataServices();
            return ds.ReadProjByOrg(orgName);
        }
        public List<ProjectTable> ReadProjByOrgandStatus(int selectedsta,string orgName)
        {
            DataServices ds = new DataServices();
            return ds.ReadProjByOrgandStatus(selectedsta,orgName);
        }
        public List<ProjectTable> ReadProByStatus(int selectedsta)
        {
            DataServices ds = new DataServices();
            return ds.ReadProByStatus(selectedsta);
        }

        public List<ProjectTable> ReadProByStatusSpe(int selectedsta, string spe)
        {
            DataServices ds = new DataServices();
            return ds.ReadProByStatusSpe(selectedsta,spe);
        }

        public List<ProjectTable> ReadProBySpe(string spe)
        {
            DataServices ds = new DataServices();
            return ds.ReadProBySpe(spe);
        }
    }
}