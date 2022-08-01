using Final_project.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_project.Models
{
    public class Student
    {
        string phoneNumber;
        string studentId;
        int specializationId;
        string teamId;
        string firstName;
        string lastName;
        bool studentStatus;

        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public string StudentId { get => studentId; set => studentId = value; }
        public int SpecializationId { get => specializationId; set => specializationId = value; }
        public string TeamId { get => teamId; set => teamId = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public bool StudentStatus { get => studentStatus; set => studentStatus = value; }

        public Student() { }

        public Student(string phoneNumber, string studentId, int specializationId, string teamId, string firstName, string lastName, bool studentStatus)
        {
            this.PhoneNumber = phoneNumber;
            this.StudentId = studentId;
            this.SpecializationId = specializationId;
            this.TeamId = teamId;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.StudentStatus = studentStatus;
        }

        public List<Student> GetStudentsList()
        {
            DataServices ds = new DataServices();
            return ds.GetStudentsList();
        }

        public List<Student> GetStudentsListTeams()
        {
            DataServices ds = new DataServices();
            return ds.GetStudentsListTeams();
        }

        public int InsertStu()
        {
            DataServices ds = new DataServices();
            int status = ds.InsertStu(this);
            return status;
        }

        public void UpdateTeam2(string student1, string student2, int teamid, string spe)
        {
            DataServices ds = new DataServices();
            ds.UpdateTeam2(student1, student2, teamid,spe);
        }

        public void UpdateTeam3(string student1, string student2, string student3, int teamid,string spe)
        {
            DataServices ds = new DataServices();
            ds.UpdateTeam3(student1, student2, student3, teamid,spe);
        }

        public void UpdateTeam4(string student1, string student2, string student3, string student4, int teamid, string spe)
        {
            DataServices ds = new DataServices();
            ds.UpdateTeam4(student1, student2, student3, student4, teamid,spe);
        }

        public void UpdateTeam(int teamid)
        {
            DataServices ds = new DataServices();
            ds.UpdateTeam(teamid);
        }

        public void UpdateStatus(string StuId)
        {
            DataServices ds = new DataServices();
            ds.UpdateStuStatus(StuId);
        }

        public void UpdateAllStu()
        {
            DataServices ds = new DataServices();
            ds.UpdateAllStu();
        }
    }
}