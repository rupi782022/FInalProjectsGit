using Final_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Final_project.Controllers
{
    public class ProjectBookController : ApiController
    {
        // GET api/<controller>
        public List<ProjectBook> Get()
        {
            ProjectBook PB = new ProjectBook();
            return PB.GetProjectBooks();
        }

        public List<ProjectBook> Get(string spe)
        {
            ProjectBook p = new ProjectBook();
            return p.ReadProBySpe(spe);
        }

        public List<ProjectBook> Get(string org,int dummy)
        {
            ProjectBook p = new ProjectBook();
            return p.ReadProByOrgsp(org);
        }
        public List<ProjectBook> Get(string org, string spe)
        {
            ProjectBook p = new ProjectBook();
            return p.ReadProByOrgSpe(org, spe);
        }

    }
}