using MediatR;

namespace Incident.Application.Incidents.Create.v1;

public sealed record CreateIncidentCommand(
 string ticketCode,
             string ticketSubject,
             string ticketDescription,
             string ticketDescriptionTextFormat,
             int? agentId,
             int? agentGroupId
) : IRequest<Guid>;