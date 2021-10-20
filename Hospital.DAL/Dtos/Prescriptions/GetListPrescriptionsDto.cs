namespace Hospital.DAL.Dtos.Prescriptions
{
    public class GetListPrescriptionsDto
    {
        public int Id { get; set; }
        public string MedicineName { get; set; }
        public int Dose { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string PatientSurname { get; set; }
    }
}
