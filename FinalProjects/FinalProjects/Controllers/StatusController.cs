using Final_project.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Final_project.Controllers
{
    public class StatusController : ApiController
    {
        // GET api/<controller>
        public int Get(string StatusText)
        {
            Status st = new Status();
            return st.ReadId(StatusText);
        }


        public List<Status> Get()
        {
             Status s = new Status();
            return s.Read();
        }

    }
}