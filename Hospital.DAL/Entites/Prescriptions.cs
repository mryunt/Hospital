using AppCore.Entity;

namespace Hospital.DAL.Entites
{
    public class Prescriptions : Audit, IEntity, ISoftDeleted
    {
        public int Id { get; set; }
        public string MedicineName { get; set; }
        public int Dose { get; set; }
        public int PatientId { get; set; }
        public Patient PatientFK { get; set; }
        public bool IsDeleted { get; set; }
    }
}
