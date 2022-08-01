using Final_project.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Final_project.Controllers
{
    public class ProjectStatusController : ApiController
    {
        // GET api/<controller>
        public int Get(string StatusProText)
        {
            ProjectStatus st = new ProjectStatus();
            return st.ReadId(StatusProText);
        }

        public List<ProjectStatus> Get()
        {
            ProjectStatus s = new ProjectStatus();
            return s.Read();
        }

    }
}