using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Final_project.Models;


namespace Final_project.Controllers
{
    public class OrgContactsController : ApiController
    {
        // GET api/<controller>
        public List<OrgContact> Get()
        {
            OrgContact OC = new OrgContact();
            return OC.getContacts();
        }

        // GET api/<controller>/5
        public List<OrgContact> Get(string orgName)
        {
            OrgContact OC = new OrgContact();
           return OC.getContactsOrg(orgName);
        }

        public OrgContact Get(string Phone, int dummy)
        {
            OrgContact OC = new OrgContact();
            return OC.getContactByPhone(Phone);
        }

        // POST api/<controller>
        public int Post([FromBody] OrgContact orgContact)
        {
            return orgContact.Insertcon();
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }
        public void Put([FromBody] OrgContact contact)
        {
            contact.Update(contact);
        }
        public void Put(string organizationName, string majorPhone)
        {
            OrgContact o = new OrgContact();
            o.UpdateMajor(organizationName, majorPhone);
        }
        public void Put(string orgName)
        {
            OrgContact o = new OrgContact();
            o.DeleteOrgContact(orgName);
        }

        // DELETE api/<controller>/5
        public void Put(string contPhone, int StatusId)
        {
            OrgContact o = new OrgContact();
            o.UpdateConStatus(contPhone, StatusId);
        }
    }
}