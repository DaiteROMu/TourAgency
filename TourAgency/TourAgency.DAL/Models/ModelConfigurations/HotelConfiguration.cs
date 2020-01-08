using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TourAgency.DAL.Models.ModelConfigurations
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.Property(prop => prop.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(prop => prop.Address)
                .IsRequired();

            builder.Property(prop => prop.PhoneNumber)
                .HasMaxLength(15)
                .IsRequired();
        }
    }
}
