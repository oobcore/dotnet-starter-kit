using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using FSH.Framework.Core.Domain.Contracts;
using FSH.Framework.Core.Persistence;
using Mapster;

namespace Incident.Infrastructure.Persistence;

/// <summary>
/// IncidentRepository, IncidentDbContext üzerinden Incident aggregate root'ları için generic repository işlemlerini sağlar.
/// Ardalis.Specification ve Mapster ile birlikte çalışır.
/// </summary>
internal sealed class IncidentRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T>
    where T : class, IAggregateRoot
{
    /// <summary>
    /// Repository, IncidentDbContext ile başlatılır.
    /// </summary>
    public IncidentRepository(IncidentDbContext context)
        : base(context)
    {
    }

    /// <summary>
    /// DTO'ya map işlemi için Mapster'ın ProjectToType fonksiyonunu kullanır.
    /// Eğer specification'da selector yoksa, veritabanı sorgusunu doğrudan DTO'ya map eder.
    /// </summary>
    protected override IQueryable<TResult> ApplySpecification<TResult>(ISpecification<T, TResult> specification) =>
        specification.Selector is not null
            ? base.ApplySpecification(specification)
            : ApplySpecification(specification, false)
                .ProjectToType<TResult>();
}
