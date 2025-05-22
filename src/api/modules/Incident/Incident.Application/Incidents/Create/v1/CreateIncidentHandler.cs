using FSH.Framework.Core.Persistence;
using Incident.Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Incident.Application.Incidents.Create.v1;

/// <summary>
/// CreateIncidentHandler, yeni bir Incident kaydı oluşturmak için CQRS pattern'ı kapsamında kullanılan bir Command Handler'dır.
/// - MediatR ile IRequestHandler arayüzünü uygular.
/// - Dependency Injection ile repository ve logger bağımlılıklarını alır.
/// - Factory Method Pattern ile Incident nesnesi oluşturur.
/// </summary>
public sealed class CreateIncidentHandler(
    ILogger<CreateIncidentHandler> logger,
    [FromKeyedServices("incident:incidents")] IRepository<Incident.Domain.Incident> repository)
    : IRequestHandler<CreateIncidentCommand, Guid>
{
    /// <summary>
    /// Handle metodu, CreateIncidentCommand komutunu işler ve yeni Incident kaydını veritabanına ekler.
    /// </summary>
    /// <param name="request">Oluşturulacak Incident bilgilerini içeren komut.</param>
    /// <param name="cancellationToken">İptal tokenı.</param>
    /// <returns>Oluşturulan Incident'in Id'si.</returns>
    public async Task<Guid> Handle(CreateIncidentCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        // Factory Method Pattern ile Incident nesnesi oluşturuluyor.
        var incident = Domain.Incident.Create(
            request.ticketCode,
            request.ticketSubject,
            request.ticketDescription,
            request.ticketDescriptionTextFormat,
            request.agentId,
            request.agentGroupId
        );
        // Repository Pattern ile veritabanına ekleniyor.
        await repository.AddAsync(incident, cancellationToken);
        // Loglama işlemi.
        logger.LogInformation("incident created {IncidentId}", incident.Id);

        return incident.Id;
    }
}
