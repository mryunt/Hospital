using AppCore.Entity;
using System.Collections.Generic;

namespace Hospital.DAL.Entites
{
    public class Patient : Audit, IEntity, ISoftDeleted
    {
        public int Id { get; set; }
        public string TcNo { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string HealthInsurance { get; set; }
        public ICollection<Prescriptions> Prescriptions { get; set; }
        public ICollection<Disease> Diseases { get; set; }
        public bool IsDeleted { get; set; }
    }
}
