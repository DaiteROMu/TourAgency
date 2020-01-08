using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TourAgency.DAL.Models.ModelConfigurations
{
    public class HotelRoomPhotoConfiguration : IEntityTypeConfiguration<HotelRoomPhoto>
    {
        public void Configure(EntityTypeBuilder<HotelRoomPhoto> builder)
        {
            builder.Property(prop => prop.BaseDirectoryName)
                .IsRequired();

            builder.Property(prop => prop.DirectoryName)
                .IsRequired();

            builder.Property(prop => prop.ImageName)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
