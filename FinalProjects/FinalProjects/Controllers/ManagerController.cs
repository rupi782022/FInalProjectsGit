using Final_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Final_project.Controllers
{
    public class ManagerController : ApiController
    {

        // POST api/<controller>
        public string Post([FromBody] Manager m)
        {
            return m.LoginManager();
        }

    }
}