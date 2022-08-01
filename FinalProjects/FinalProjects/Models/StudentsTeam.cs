using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Final_project.Models.DAL;


namespace Final_project.Models
{
    public class StudentsTeam
    {
        string teamId;
        string firstName;
        string lastName;
        bool isAccepted;
        string additionalInfo;
        string specializationName;
        string phoneNumber;



        public StudentsTeam() { }

        public StudentsTeam(string teamId, string firstName, string lastName, bool isAccepted, string additionalInfo, string specializationName, string phoneNumber)
        {
            this.TeamId = teamId;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.IsAccepted = isAccepted;
            this.AdditionalInfo = additionalInfo;
            this.SpecializationName = specializationName;
            this.PhoneNumber = phoneNumber;
        }

        public string TeamId { get => teamId; set => teamId = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public bool IsAccepted { get => isAccepted; set => isAccepted = value; }
        public string AdditionalInfo { get => additionalInfo; set => additionalInfo = value; }
        public string SpecializationName { get => specializationName; set => specializationName = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }

        public List<StudentsTeam> ReadStuByTeam()
        {
            DataServices ds = new DataServices();
            return ds.ReadStuByTeam();
        }

        public List<StudentsTeam> ReadTeamSt(string teamid)
        {
            DataServices ds = new DataServices();
            return ds.ReadTeamSt(teamid);
        }
        public List<StudentsTeam> ReadSpeTeam(string spe)
        {
            DataServices ds = new DataServices();
            return ds.ReadSpeTeam(spe);
        }
    }
}