using Finbuckle.MultiTenant.Abstractions;
using FSH.Framework.Core.Persistence;
using FSH.Framework.Infrastructure.Persistence;
using FSH.Framework.Infrastructure.Tenant;
using Incident.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shared.Constants;

namespace Incident.Infrastructure.Persistence;

/// <summary>
/// IncidentDbContext, Incident modülündeki veritabanı işlemlerini yönetir.
/// FshDbContext'ten türetilmiştir ve çoklu tenant, event publishing gibi altyapı özelliklerini destekler.
/// </summary>
public sealed class IncidentDbContext : FshDbContext
{
    /// <summary>
    /// Constructor, çoklu tenant, db seçenekleri ve event publisher gibi bağımlılıkları alır.
    /// </summary>
    public IncidentDbContext(
        IMultiTenantContextAccessor<FshTenantInfo> multiTenantContextAccessor,
        DbContextOptions<IncidentDbContext> options,
        IPublisher publisher,
        IOptions<DatabaseOptions> settings)
        : base(multiTenantContextAccessor, options, publisher, settings)
    {
    }

    /// <summary>
    /// Incident entity'si için DbSet tanımı.
    /// </summary>
    public DbSet<Domain.Incident> Incidents { get; set; } = null!;

    /// <summary>
    /// Model oluşturulurken konfigürasyonları ve şema adını uygular.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IncidentDbContext).Assembly);
        modelBuilder.HasDefaultSchema(SchemaNames.Incident); // Şema adını ayarlayın (ör: "Incident")
    }
}
