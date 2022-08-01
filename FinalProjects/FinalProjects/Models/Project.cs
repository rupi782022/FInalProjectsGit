using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Final_project.Models.DAL;

namespace Final_project.Models
{
    public class Project
    {
        string projectName;
        string projectDescription;
        string url;
        string notes;
        int statusId;
        int specializationId;

        public Project(string projectName, string projectDescription, string url, string notes,int statusId,int specializationId)
        {
            this.ProjectName = projectName;
            this.ProjectDescription = projectDescription;
            this.Url = url;
            this.Notes = notes;
            this.StatusId = statusId;
            this.SpecializationId = specializationId;

        }

        public Project() { }

        public string ProjectName { get => projectName; set => projectName = value; }        
        public string ProjectDescription { get => projectDescription; set => projectDescription = value; }
        public string Url { get => url; set => url = value; }
        public string Notes { get => notes; set => notes = value; }
        public int StatusId { get => statusId; set => statusId=value; }
        public int SpecializationId { get => specializationId; set =>  specializationId=value; }

        public int InsertPro()
        {
            DataServices ds = new DataServices();
            int status = ds.InsertPro(this);
            return status;
        }

        public void deleteproj(string proName)
        {
            DataServices ds = new DataServices();
            ds.deleteproj(proName);
        }

        public void Update(Project pro)
        {
            DataServices ds = new DataServices();
            ds.UpdatePro(pro);
        }

    }
}