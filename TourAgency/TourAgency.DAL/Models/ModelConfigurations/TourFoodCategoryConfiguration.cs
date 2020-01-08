using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace TourAgency.DAL.Models.ModelConfigurations
{
    public class TourFoodCategoryConfiguration : IEntityTypeConfiguration<TourFoodCategory>
    {
        public void Configure(EntityTypeBuilder<TourFoodCategory> builder)
        {
            builder.Property(prop => prop.ShortName)
                .IsRequired()
                .HasMaxLength(5);

            builder.Property(prop => prop.FullName)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
