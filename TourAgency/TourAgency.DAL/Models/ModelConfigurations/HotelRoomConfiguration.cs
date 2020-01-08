using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TourAgency.DAL.Models.ModelConfigurations
{
    public class HotelRoomConfiguration : IEntityTypeConfiguration<HotelRoom>
    {
        public void Configure(EntityTypeBuilder<HotelRoom> builder)
        {
            builder.Property(prop => prop.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(prop => prop.PersonsNumber)
                .IsRequired();

            builder.Property(prop => prop.Square)
                .IsRequired();

            builder.Property(prop => prop.Options)
                .IsRequired();
        }
    }
}
