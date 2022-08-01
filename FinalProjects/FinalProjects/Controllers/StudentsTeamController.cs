using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Final_project.Models;


namespace Final_project.Controllers
{
    public class StudentsTeamController : ApiController
    {
        public List<StudentsTeam> Get()
        {
            StudentsTeam st = new StudentsTeam();
            return st.ReadStuByTeam();
        }
        public List<StudentsTeam> Get(string teamid)
        {
            StudentsTeam st = new StudentsTeam();
            return st.ReadTeamSt(teamid);
        }

        public List<StudentsTeam> Get(string spe,int dummy)
        {
            StudentsTeam st = new StudentsTeam();
            return st.ReadSpeTeam(spe);
        }

    }
}