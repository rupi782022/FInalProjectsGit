using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Final_project.Models.DAL;

namespace Final_project.Models
{
    public class Presentation
    {
        string judge1PhoneNumber;
        string judge2PhoneNumber;
        int presentationId;
        string teamId;
        DateTime startDateAndTime;
        string building;
        string classS;

        public Presentation(string judge1PhoneNumber, string judge2PhoneNumber, int presentationId, string teamId, DateTime startDateAndTime, string building, string classS)
        {
            this.Judge1PhoneNumber = judge1PhoneNumber;
            this.Judge2PhoneNumber = judge2PhoneNumber;
            this.PresentationId = presentationId;
            this.TeamId = teamId;
            this.StartDateAndTime = startDateAndTime;
            this.Building = building;
            this.ClassS = classS;
        }

        public string Judge1PhoneNumber { get => judge1PhoneNumber; set => judge1PhoneNumber = value; }
        public string Judge2PhoneNumber { get => judge2PhoneNumber; set => judge2PhoneNumber = value; }
        public int PresentationId { get => presentationId; set => presentationId = value; }
        public string TeamId { get => teamId; set => teamId = value; }
        public DateTime StartDateAndTime { get => startDateAndTime; set => startDateAndTime = value; }
        public string Building { get => building; set => building = value; }
        public string ClassS { get => classS; set => classS = value; }

        public Presentation() { }

        public int InsertPre()
        {
            DataServices ds = new DataServices();
            int status = ds.InsertPre(this);
            return status;
        }

        public List<Presentation> ReadPresentation()
        {
            DataServices ds = new DataServices();
            return ds.ReadPresentation();
        }
        public void DeletePresentations(Presentation presentation)
        {
            DataServices ds = new DataServices();
             ds.DeletePresentations();
        }
    }
}

    