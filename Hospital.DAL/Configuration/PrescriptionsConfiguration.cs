using Hospital.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL.Configuration
{
    public class PrescriptionsConfiguration : IEntityTypeConfiguration<Prescriptions>
    {
        public void Configure(EntityTypeBuilder<Prescriptions> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.MedicineName).IsRequired();
            builder.Property(p => p.Dose).IsRequired();
            builder.Property(p => p.PatientId).IsRequired();

            builder.HasOne(p => p.PatientFK).WithMany(p => p.Prescriptions).HasForeignKey(p => p.PatientId);
        }
    }
}
