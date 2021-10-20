using AppCore.Entity;
using System.Collections.Generic;

namespace Hospital.DAL.Entites
{
    public class Doctor : Audit, IEntity, ISoftDeleted
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Seniority { get; set; }
        public string Departmen { get; set; }
        public ICollection<Disease> Diseases { get; set; }
        public bool IsDeleted { get; set; }
    }
}
