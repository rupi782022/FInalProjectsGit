using Final_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Final_project.Controllers
{
    public class MentorsController : ApiController
    {
        // GET api/<controller>
        public List<Mentor> Get()
        {
            Mentor m = new Mentor();
            return m.GetMentorsList();
        }
        // GET api/<controller>/5
        public List<Mentor> Get(string spe)
        {
            Mentor m = new Mentor();
            return m.ReadSpeMentor(spe);
        }

        // POST api/<controller>
        public int Post([FromBody] Mentor mentor)
        {
            return mentor.InsertMen();
        }

        // PUT api/<controller>/5
        public void Put(string Phone)
        {
            Mentor m = new Mentor();
            m.Update(Phone);
        }

        public void Put()
        {
            Mentor m = new Mentor();
            m.UpdateAll();
        }

    }
}