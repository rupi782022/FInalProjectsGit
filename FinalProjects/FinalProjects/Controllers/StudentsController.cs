using Final_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Final_project.Controllers
{
    public class StudentsController : ApiController
    {
        // GET api/<controller>
        public List<Student> Get()
        {
            Student s = new Student();
            return s.GetStudentsList();
        }

        public List<Student> Get(int dum)
        {
            Student s = new Student();
            return s.GetStudentsListTeams();
        }



        // POST api/<controller>
        public int Post([FromBody] Student student)
        {
            return student.InsertStu();
        }

        // PUT api/<controller>/5

        public void Put()
        {
            Student s = new Student();
            s.UpdateAllStu();
        }

        public void Put(int teamId)
        {
            Student s = new Student();
            s.UpdateTeam(teamId);
        }

        public void Put(string StuId)
        {
            Student s = new Student();
            s.UpdateStatus(StuId);
        }
        public void Put(string student1, string student2, int teamid, string spe)
        {
            Student s = new Student();
            s.UpdateTeam2(student1,student2,teamid,spe);
        }

        public void Put(string student1, string student2, string student3, int teamid, string spe)
        {
            Student s = new Student();
            s.UpdateTeam3(student1, student2,student3, teamid,spe);
        }

        public void Put(string student1, string student2, string student3, string student4, int teamid, string spe)
        {
            Student s = new Student();
            s.UpdateTeam4(student1, student2, student3, student4, teamid,spe);
        }

    }
}