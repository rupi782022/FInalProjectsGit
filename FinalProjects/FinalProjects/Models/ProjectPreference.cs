using Final_project.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_project.Models
{
    public class ProjectPreference
    {
        string teamId;
        string projectName;
        int preference;
        bool suggestedSolution;
        bool finalSolution;

        public ProjectPreference(string teamId, string projectName, int preference, bool suggestedSolution, bool finalSolution)
        {
            this.TeamId = teamId;
            this.ProjectName = projectName;
            this.Preference = preference;
            this.SuggestedSolution = suggestedSolution;
            this.FinalSolution = finalSolution;
        }

        public string TeamId { get => teamId; set => teamId = value; }
        public string ProjectName { get => projectName; set => projectName = value; }
        public int Preference { get => preference; set => preference = value; }
        public bool SuggestedSolution { get => suggestedSolution; set => suggestedSolution = value; }
        public bool FinalSolution { get => finalSolution; set => finalSolution = value; }

        public ProjectPreference()
        {

        }
        public ProjectPreference(string teamId, string projectName, int preference)
        {
            this.TeamId = teamId;
            this.ProjectName = projectName;
            this.Preference = preference;
        }

        public int InsertProjectPreference()
        {
            DataServices ds = new DataServices();
            int status = ds.InsertProjectPreference(this);
            return status;
        }
        public void DeletePreference()
        {
            DataServices ds = new DataServices();
            ds.DeletePreference(this);
        }

        public bool ReadPreferenceByTeam(string teamId,string finalProj)
        {
            DataServices ds = new DataServices();
            return ds.ReadPreferenceByTeam(teamId, finalProj);
        }

        public List<ProjectPreference> ReadPreferenceByTeamId(string teamId)
        {
            DataServices ds = new DataServices();
            return ds.ReadPreferenceByTeamId(teamId);
        }

        public List<ProjectPreference> ReadProByMen(string menPhone)
        {
            DataServices ds = new DataServices();
            return ds.ReadProByMen(menPhone);
        }
        public List<ProjectPreference> ReadAllPreference()
        {
            DataServices ds = new DataServices();
            return ds.ReadAllPreference();
        }
         
               public void UpdatePreference(string TeamId, string Project)
        {
            DataServices ds = new DataServices();
            ds.UpdatePreference(TeamId, Project);
        }
        public void UpdatePreferenceFinal(string TeamId, string FinalProj, string mentor)
        {
            DataServices ds = new DataServices();
            ds.UpdatePreferenceFinal(TeamId, FinalProj,mentor);
        }
        

    }
}