using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Final_project.Models.DAL;

namespace Final_project.Models
{
    public class Team
    {
        string teamId;
          bool isAccepted;
        string mail ;
        string password ;
        string mentorPhoneNumber;
        string additionalInfo;
        string specializationName;

        public string TeamId { get => teamId; set => teamId = value; }
        public bool IsAccepted { get => isAccepted; set => isAccepted = value; }
        public string Mail { get => mail; set => mail = value; }
        public string Password { get => password; set => password = value; }
        public string MentorPhoneNumber { get => mentorPhoneNumber; set => mentorPhoneNumber = value; }
        public string AdditionalInfo { get => additionalInfo; set => additionalInfo = value; }
        public string SpecializationName { get => specializationName; set => specializationName = value; }

        public Team()
        {

        }

        public Team(string teamId, string password)
        {
            this.TeamId = teamId;
            this.Password = password;
        }

        public Team(string teamId, bool isAccepted, string mail, string password, string mentorPhoneNumber, string additionalInfo, string specializationName)
        {
            this.TeamId = teamId;
            this.IsAccepted = isAccepted;
            this.Mail = mail;
            this.Password = password;
            this.MentorPhoneNumber = mentorPhoneNumber;
            this.AdditionalInfo = additionalInfo;
            this.SpecializationName = specializationName;
        }

        public void InsetTeam()
        {
            DataServices ds = new DataServices();
           ds.InsetTeam(this);
        }

        public string Login()
        {
            DataServices ds = new DataServices();
            return ds.Login(this);
        }
        public List<Team> ReadAllTeams()
        {
            DataServices ds = new DataServices();
          return  ds.ReadAllTeams();
        }

        public void UpdateTeamreason(string reason, int teamId)
        {
            DataServices ds = new DataServices();
            ds.UpdateTeamreason(reason, teamId);
        }

        public void UpdateTeamste(int status, int teamId)
        {
            DataServices ds = new DataServices();
            ds.UpdateTeamStatus(status, teamId);
        }

        public void Update(Team team)
        {
            DataServices ds = new DataServices();
            ds.UpdateTeamStatus(team);
        }
    }
}