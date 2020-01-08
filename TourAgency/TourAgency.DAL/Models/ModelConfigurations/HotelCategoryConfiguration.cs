using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TourAgency.DAL.Models.ModelConfigurations
{
    public class HotelCategoryConfiguration : IEntityTypeConfiguration<HotelCategory>
    {
        public void Configure(EntityTypeBuilder<HotelCategory> builder)
        {
            builder.Property(prop => prop.Name)
                .IsRequired()
                .HasMaxLength(10);
        }
    }
}
