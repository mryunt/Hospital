namespace Hospital.DAL.Dtos.Patient
{
    public class GetPatientDto
    {
        public int Id { get; set; }
        public string TcNo { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string HealthInsurance { get; set; }
    }
}
