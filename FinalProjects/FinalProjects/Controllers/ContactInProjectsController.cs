using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Final_project.Models;


namespace Final_project.Controllers
{
    public class ContactInProjectsController : ApiController
    {
        public List<ProjectTable> Get(string ProName)
        {
            ProjectTable pr = new ProjectTable();
            return pr.ReadProByName(ProName);
        }

        public List<ProjectTable> Get(int selectedsta)
        {
            ProjectTable p = new ProjectTable();
            return p.ReadProByStatus(selectedsta);
        }

        public List<ProjectTable> Get(string spe, int dummy)
        {
            ProjectTable p = new ProjectTable();
            return p.ReadProBySpe(spe);
        }

        public List<ProjectTable> Get(int selectedsta, string spe)
        {
            ProjectTable p = new ProjectTable();
            return p.ReadProByStatusSpe(selectedsta,spe);
        }
        public List<ProjectTable> Get()
        {
            ProjectTable p = new ProjectTable();
            return p.Read();
        }

        // POST api/<controller>
        public int Post([FromBody] ContactInProject contactInProject)
        {
            return contactInProject.InsertPro();
        }

        // PUT api/<controller>/5
        public void Put([FromBody] ContactInProject contactInProject)
        {
            contactInProject.Update(contactInProject);
        }
    }
}