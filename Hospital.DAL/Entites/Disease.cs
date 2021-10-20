using AppCore.Entity;

namespace Hospital.DAL.Entites
{
    public class Disease : Audit, IEntity, ISoftDeleted
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public Doctor DoctorFK { get; set; }
        public int PatientId { get; set; }
        public Patient PatientFK { get; set; }

        public bool IsDeleted { get; set; }
    }
}
