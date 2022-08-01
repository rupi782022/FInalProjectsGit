using Final_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Final_project.Controllers
{
    public class ProjectPreferenceController : ApiController
    {
        // GET api/<controller>
        public bool Get(string teamId,string finalProj)
        {
            ProjectPreference PP = new ProjectPreference();
            return PP.ReadPreferenceByTeam(teamId,finalProj);
        }

        public List<ProjectPreference> Get(string teamId)
        {
            ProjectPreference PP = new ProjectPreference();
            return PP.ReadPreferenceByTeamId(teamId);
        }

        public List<ProjectPreference> Get(string mentorPhone, int dummy)
        {
            ProjectPreference PP = new ProjectPreference();
            return PP.ReadProByMen(mentorPhone);
        }
        public List<ProjectPreference> Get()
        {
            ProjectPreference PP = new ProjectPreference();
            return PP.ReadAllPreference();
        }

        // POST api/<controller>
        public int Post([FromBody] ProjectPreference projectPreference)
        {
            return projectPreference.InsertProjectPreference();
        }

        // PUT api/<controller>/5
        public void Put(string TeamId, string Project)
        {
            ProjectPreference PP = new ProjectPreference();
            PP.UpdatePreference(TeamId, Project);
        }
        public void Put(string TeamId, string FinalProj, string mentor)
        {
            ProjectPreference PP = new ProjectPreference();
            PP.UpdatePreferenceFinal(TeamId, FinalProj, mentor);
        }

        // DELETE api/<controller>/5
        public void Delete([FromBody] ProjectPreference projectPreference)
        {
            projectPreference.DeletePreference();
        }

    }
}