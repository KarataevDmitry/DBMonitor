
using DBMonitor.BLL;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DBMonitor.DAL.Configurations
{
    public class LaunchHistoryConfiguration : IEntityTypeConfiguration<LaunchHistory>
    {
        public void Configure(EntityTypeBuilder<LaunchHistory> builder) => builder.HasKey(x => x.Id);
    }
}
