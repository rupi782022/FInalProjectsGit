using Final_project.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Final_project.Controllers
{
    public class SpecializationController : ApiController
    {
        // GET api/<controller>
        public List<Specialization> Get()
        {
            Specialization sp = new Specialization();
            return sp.Read();
        }

        public int Get(string SpeText)
        {
            Specialization spe = new Specialization();
            return spe.ReadId(SpeText);
        }

    }
}