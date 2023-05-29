using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pay.OvetimePolicies.Domain.Entities;

namespace Pay.OvetimePolicies.Application.Configs
{

    public class PayConfig : IEntityTypeConfiguration<PayEntity>
    {

        public void Configure(EntityTypeBuilder<PayEntity> builder)
        {
            builder.ToTable("Pays", "dbo");
        }
    }


}
