using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Final_project.Models.DAL
{
    public class ProjectStatus
    {
        int statusId;
        string statusText;
        public ProjectStatus() { }
        public ProjectStatus(int statusId, string statusText)
        {
            this.StatusId = statusId;
            this.StatusText = statusText;
        }

        public int StatusId { get => statusId; set => statusId = value; }
        public string StatusText { get => statusText; set => statusText = value; }
        public List<ProjectStatus> Read()
        {
            DataServices ds = new DataServices();
            return ds.ReadStatusPro();
        }

        public int ReadId(string sta)
        {
            DataServices ds = new DataServices();
            return ds.ReadStatusProId(sta);
        }
    }
}