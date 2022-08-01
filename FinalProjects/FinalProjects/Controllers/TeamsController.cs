using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Final_project.Models;

namespace Final_project.Controllers
{
    public class TeamsController : ApiController
    {
        // GET api/<controller>
        public List<Team> Get()
        {
            Team t = new Team();
            return t.ReadAllTeams();
        }

        // POST api/<controller>
        public string Post([FromBody] Team team, string str)
        {
            return team.Login();
        }

        public void Put(string reason, int teamId)
        {
            Team t = new Team();
            t.UpdateTeamreason(reason, teamId);
        }

        public void Put(int status, int teamId)
        {
            Team t = new Team();
            t.UpdateTeamste(status, teamId);
        }

        public void Put([FromBody] Team team)
        {
            team.Update(team);
        }

        public void Post([FromBody] Team team)
        {
            team.InsetTeam();
        }
    }
}