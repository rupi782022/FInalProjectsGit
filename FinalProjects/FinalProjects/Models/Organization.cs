using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Final_project.Models.DAL;

namespace Final_project.Models
{
    public class Organization
    {
        string organizationName;
        string description;
        string logo;
        string notes;
        int statusId;
        int specializationId;

        public Organization(string organizationName, string description, string logo, string notes, int statusId, int specializationId)
        {
            this.OrganizationName = organizationName;
            this.Description = description;
            this.Logo = logo;
            this.Notes = notes;
            this.StatusId = statusId;
            this.SpecializationId = specializationId;
        }

        public Organization()
        { }
        public string OrganizationName { get => organizationName; set => organizationName = value; }
        public string Description { get => description; set => description = value; }
        public string Logo { get => logo; set => logo = value; }
        public string Notes { get => notes; set => notes = value; }
        public int StatusId { get => statusId; set => statusId = value; }
        public int SpecializationId { get => specializationId; set => specializationId = value; }

        public void Update(Organization org)
        {
            DataServices ds = new DataServices();
             ds.UpdateOrg(org);
        }
        public int InsertOrg()
        {
            DataServices ds = new DataServices();
            int status = ds.InsertOrg(this);
            return status;
        }


        public int ReadIdfromDB(string orgname)
        {
            DataServices ds = new DataServices();
            return ds.ReadIdfromDB(orgname);
        }
        public void deleteOrg(string orgName)
        {
            DataServices ds = new DataServices();
            ds.deleteOrg(orgName);
        }

        //    public void UpdateOrgStatus(string orgName, string Cstatus)
        //{
        //    DataServices ds = new DataServices();
        //    ds.UpdateOrgStatus(orgName, Cstatus);
        //}



    }

}