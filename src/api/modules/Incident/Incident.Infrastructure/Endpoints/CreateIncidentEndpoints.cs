using Incident.Application.Incidents.Create.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Incident.Api.Endpoints;

public static class CreateIncidentEndpoints
{
    public static IEndpointRouteBuilder MapIncidentEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/", async (CreateIncidentCommand command, IMediator mediator) =>
        {
            var id = await mediator.Send(command);
            return Results.Created($"/api/{id}", id);
        })
        .WithName("CreateIncident")
        .WithSummary("Yeni bir Incident kaydı oluşturur.")
        .WithDescription("Incident oluşturmak için kullanılır.")
        .Produces<Guid>(201);

        return endpoints;
    }
}