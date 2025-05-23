using Incident.Application.Incidents.Create.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Incident.Api.Endpoints;

/// <summary>
/// Incident oluşturma endpointlerini tanımlar.
/// Bu endpoint, POST /api/v1/incidents yolunda yeni bir Incident kaydı oluşturur.
/// </summary>
public static class CreateIncidentEndpoints
{
    /// <summary>
    /// Incident oluşturma endpointini haritalar.
    /// </summary>
    public static IEndpointRouteBuilder MapIncidentEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/", async (CreateIncidentCommand command, IMediator mediator) =>
        {
            // Endpoint tetiklendiğinde log ekleyebilirsin
            Console.WriteLine("CreateIncident endpoint hit!");

            var id = await mediator.Send(command);
            // Daha anlamlı bir location header ile dön
            return Results.Created($"/api/v1/incidents/{id}", id);
        })
        .WithName("CreateIncident")
        .WithSummary("Yeni bir Incident kaydı oluşturur.")
        .WithDescription("Incident oluşturmak için kullanılır.")
        .Produces<Guid>(201);

        return endpoints;
    }
}