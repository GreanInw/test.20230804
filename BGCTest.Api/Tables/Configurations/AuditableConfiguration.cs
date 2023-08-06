using BGCTest.Api.Tables.Auditables;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BGCTest.Api.Tables.Configurations
{
    public class AuditableConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : class, IAuditableEntity
    {
        public const int DefaultMaxLength = 256;
        public static IEnumerable<Type> AllowTypes => new[]
        {
            typeof(IAuditableEntity)
        };

        public int MaxLength { get; }

        public AuditableConfiguration() : this(DefaultMaxLength) { }

        public AuditableConfiguration(int maxLength)
        {
            MaxLength = maxLength < 1 ? DefaultMaxLength : maxLength;
        }

        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            var entityInterfaces = typeof(TEntity).GetInterfaces();
            if (!entityInterfaces.Any(c => AllowTypes.Contains(c)))
            {
                throw new Exception($"Class : '{typeof(TEntity).Name}' not inherited '{nameof(IAuditableEntity)}'.");
            }

            builder.Property(nameof(IAuditableEntity.CreatedBy)).HasMaxLength(MaxLength).IsRequired();
            builder.Property(nameof(IAuditableEntity.CreatedDate)).IsRequired();

            if (entityInterfaces.Any(w => w == typeof(IAuditableEntity)))
            {
                builder.Property(nameof(IAuditableEntity.ModifiedBy)).HasMaxLength(MaxLength).IsRequired();
                builder.Property(nameof(IAuditableEntity.ModifiedBy)).IsRequired();
            }
        }
    }
}