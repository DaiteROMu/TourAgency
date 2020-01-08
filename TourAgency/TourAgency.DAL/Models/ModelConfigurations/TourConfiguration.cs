using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TourAgency.DAL.Models.ModelConfigurations
{
    public class TourConfiguration : IEntityTypeConfiguration<Tour>
    {
        public void Configure(EntityTypeBuilder<Tour> builder)
        {
            builder.Property(prop => prop.Term)
                .IsRequired();

            builder.Property(prop => prop.Price)
                .IsRequired();
        }
    }
}
