using Final_project.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_project.Models
{
    public class Mentor
    {
        string phoneNumber;
        string firstName;
        string lastName;
        string notes;
        string mail;
        string teamId;
        int specializationId;
        bool mentorStatus;
        bool isJudge;

        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Notes { get => notes; set => notes = value; }
        public string Mail { get => mail; set => mail = value; }
        public string TeamId { get => teamId; set => teamId = value; }
        public int SpecializationId { get => specializationId; set => specializationId = value; }
        public bool MentorStatus { get => mentorStatus; set => mentorStatus = value; }
        public bool IsJudge { get => isJudge; set => isJudge = value; }

        public Mentor()
        {

        }

        public Mentor(string phoneNumber, string firstName, string lastName, string notes, string mail, string teamId, int specializationId, bool mentorStatus, bool isJudge)
        {
            this.PhoneNumber = phoneNumber;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Notes = notes;
            this.Mail = mail;
            this.TeamId = teamId;
            this.SpecializationId = specializationId;
            this.MentorStatus = mentorStatus;
            this.IsJudge = isJudge;
        }

        public List<Mentor> GetMentorsList()
        {
            DataServices ds = new DataServices();
            return ds.GetMentorsList();
        }

        public List<Mentor> ReadSpeMentor(string spe)
        {
            DataServices ds = new DataServices();
            return ds.ReadSpeMen(spe);
        }

        public int InsertMen()
        {
            DataServices ds = new DataServices();
            int status = ds.InsertMen(this);
            return status;
        }

        public void Update(string Phone)
        {
            DataServices ds = new DataServices();
            ds.UpdateMentor(Phone);
        }

        public void UpdateAll()
        {
            DataServices ds = new DataServices();
            ds.UpdateAll();
        }

    }
}