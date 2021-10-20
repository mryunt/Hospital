using Hospital.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.DAL.Configuration
{
    public class DiseaseConfiguration : IEntityTypeConfiguration<Disease>
    {
        public void Configure(EntityTypeBuilder<Disease> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.DoctorId).IsRequired();
            builder.Property(p => p.PatientId).IsRequired();

            builder.HasOne(p => p.DoctorFK).WithMany(p => p.Diseases).HasForeignKey(p => p.DoctorId);
            builder.HasOne(p => p.PatientFK).WithMany(p => p.Diseases).HasForeignKey(p => p.PatientId);
        }
    }
}
