namespace Hospital.DAL.Dtos.Prescriptions
{
    public class UpdatePrescriptionsDto
    {
        public string MedicineName { get; set; }
        public int Dose { get; set; }
        public int PatientId { get; set; }
    }
}
