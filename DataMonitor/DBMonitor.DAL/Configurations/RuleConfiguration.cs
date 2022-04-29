
using DBMonitor.BLL;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DBMonitor.DAL.Configurations
{
    public class RuleConfiguration : IEntityTypeConfiguration<Rule>
    {
        public void Configure(EntityTypeBuilder<Rule> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.QueryText);
            builder.Property(x => x.Description);
            builder.Property(x => x.AddedByUser);
            builder.Property(x => x.DeletedAt);
            builder.Property(x => x.RunAt);
            builder.Property(x => x.Name).IsRequired();
            builder.HasMany(x => x.LaunchHistories).WithOne(x => x.Rule);


        }
    }
}
