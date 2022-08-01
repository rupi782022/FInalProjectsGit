using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Final_project.Models;

namespace Final_project.Controllers
{
    public class PresentationController : ApiController
    {
        // GET api/<controller>
        public List<Presentation> Get()
        {
            Presentation p = new Presentation();
            return p.ReadPresentation();
        }
        public PresentationsDetails Get(int type)
        {
            PresentationsDetails ps = new PresentationsDetails();
            return ps.ReadPresentationsDetails();
        }

        // POST api/<controller>
        public int Post([FromBody] Presentation presentation)
        {
            return presentation.InsertPre();
        }
        public int Post([FromBody] PresentationsDetails PresentationsDetails,string type)
        {
            return PresentationsDetails.InsertPresentationsDetails();
        }
        public void Delete([FromBody] Presentation presentation)
        {
            presentation.DeletePresentations(presentation);
        }
    }
}