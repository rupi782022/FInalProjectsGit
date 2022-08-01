using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Final_project.Models;

namespace Final_project.Controllers
{
    public class OrganizationsController : ApiController
    {
        public OrganizationTable Get(string orgName)
        {
            OrganizationTable o = new OrganizationTable();
            return o.ReadOrgByName(orgName);
        }

        public List<OrganizationTable> Get()
        {
            OrganizationTable o = new OrganizationTable();
            return o.Read() ;
        }

        public List<OrganizationTable> Get(int selectedsta)
        {
            OrganizationTable o = new OrganizationTable();
            return o.ReadOrgs(selectedsta);
        }

        // DELETE api/<controller>/5
        public void Put(string orgName)
        {
            Organization O = new Organization();
            O.deleteOrg(orgName);
        }

        public int Post([FromBody] Organization organization)
        {
            return organization.InsertOrg();
        }

        public void Put([FromBody] Organization org)
        {
            org.Update(org);
        }

    }
}