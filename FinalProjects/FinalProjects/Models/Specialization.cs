using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_project.Models.DAL
{
    public class Specialization
    {
        int specializationId;
        string specializationName;
        string specializationType;

        public Specialization() { }

        public Specialization(int specializationId, string specializationName, string specializationType)
        {
            this.SpecializationId = specializationId;
            this.SpecializationName = specializationName;
            this.SpecializationType = specializationType;
        }

        public int SpecializationId { get => specializationId; set => specializationId = value; }
        public string SpecializationName { get => specializationName; set => specializationName = value; }
        public string SpecializationType { get => specializationType; set => specializationType = value; }

        public List<Specialization> Read()
        {
            DataServices ds = new DataServices();
            return ds.ReadSpecial();
        }

        public int ReadId(string spe)
        {
            DataServices ds = new DataServices();
            return ds.ReadSpeId(spe);
        }
    }
}