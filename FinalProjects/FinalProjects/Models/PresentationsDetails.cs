using Final_project.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_project.Models
{
    public class PresentationsDetails
    {
        string pres1;
        string pres2;
        string pres3;

        public PresentationsDetails(string pres1, string pres2, string pres3)
        {
            this.Pres1 = pres1;
            this.Pres2 = pres2;
            this.Pres3 = pres3;
        }

        public string Pres1 { get => pres1; set => pres1 = value; }
        public string Pres2 { get => pres2; set => pres2 = value; }
        public string Pres3 { get => pres3; set => pres3 = value; }

        public PresentationsDetails() { }

        public int InsertPresentationsDetails()
        {
            DataServices ds = new DataServices();
            int status = ds.InsertPresentationsDetails(this);
            return status;
        }

        public PresentationsDetails ReadPresentationsDetails()
        {
            DataServices ds = new DataServices();
            return ds.ReadPresentationsDetails();
        }
    }
}