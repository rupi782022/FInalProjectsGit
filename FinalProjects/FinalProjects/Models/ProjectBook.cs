using Final_project.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_project.Models
{
    public class ProjectBook
    {
        string organizationName;
        string orgDescription;
        string logo;
        string projectName;
        string projectDescription;
        string statusText;
        string specializationName;
        string orgSpecializationName;

        public string OrganizationName { get => organizationName; set => organizationName = value; }
        public string OrgDescription { get => orgDescription; set => orgDescription = value; }
        public string Logo { get => logo; set => logo = value; }
        public string ProjectName { get => projectName; set => projectName = value; }
        public string ProjectDescription { get => projectDescription; set => projectDescription = value; }
        public string StatusText { get => statusText; set => statusText = value; }
        public string SpecializationName { get => specializationName; set => specializationName = value; }
        public string OrgSpecializationName { get => orgSpecializationName; set => orgSpecializationName = value; }

        public ProjectBook() { }

        public ProjectBook(string organizationName, string orgDescription, string logo, string projectName, string projectDescription, string statusText, string specializationName, string orgSpecializationName)
        {
            this.OrganizationName = organizationName;
            this.OrgDescription = orgDescription;
            this.Logo = logo;
            this.ProjectName = projectName;
            this.ProjectDescription = projectDescription;
            this.StatusText = statusText;
            this.SpecializationName = specializationName;
            this.OrgSpecializationName = orgSpecializationName;
        }

        public List<ProjectBook> GetProjectBooks()
        {
            DataServices ds = new DataServices();
            return ds.ReadprojectBook();
        }

        public List<ProjectBook> ReadProBySpe(string spe)
        {
            DataServices ds = new DataServices();
            return ds.ReadBProBySpe(spe);
        }
        public List<ProjectBook> ReadProByOrgsp(string org)
        {
            DataServices ds = new DataServices();
            return ds.ReadProByOrgsp(org);
        }
        public List<ProjectBook> ReadProByOrgSpe(string org,string spe)
        {
            DataServices ds = new DataServices();
            return ds.ReadProByOrgSpe(org,spe);
        }
    }
}