using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Final_project.Models;

namespace Final_project.Controllers
{
    public class ProjectsController : ApiController
    {
        public List<ProjectTable> Get(string orgName)
        {
            ProjectTable PT = new ProjectTable();
            return PT.ReadProjByOrg(orgName);
        }

        public List<ProjectTable> Get(int selectedsta,string orgName)
        {
            ProjectTable PT = new ProjectTable();
            return PT.ReadProjByOrgandStatus(selectedsta,orgName);
        }

        // POST api/<controller>
        public int Post([FromBody] Project project)
        {
            return project.InsertPro();
        }

        // DELETE api/<controller>/5
        public void Put(string proName)
        {
            Project p = new Project();
            p.deleteproj(proName);
        }

        public void Put([FromBody] Project pro)
        {
            pro.Update(pro);
        }

    }
}