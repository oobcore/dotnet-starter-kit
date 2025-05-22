using Carter;
using FSH.Framework.Core.Persistence;
using FSH.Framework.Infrastructure.Persistence;
using Incident.Api.Endpoints;
using Incident.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Incident.Infrastructure;

/// <summary>
/// IncidentModule, Incident ile ilgili servislerin ve endpointlerin uygulamaya eklenmesini sağlar.
/// 
/// <para><b>Carter Nedir?</b></para>
/// Carter, .NET uygulamalarında minimal ve modüler API endpointlerini kolayca tanımlamayı sağlayan bir kütüphanedir.
/// - Klasik Controller yapısına göre daha sade ve fonksiyonel bir endpoint tanımı sunar.
/// - Her modül kendi endpointlerini ve route’larını bağımsız olarak yönetebilir.
/// - Büyük ve modüler projelerde okunabilirliği ve test edilebilirliği artırır.
/// 
/// <para><b>Bu modül ne yapar?</b></para>
/// - Carter ile endpointler modüler olarak eklenir.
/// - Repository ve DbContext bağımlılıkları DI container'a eklenir.
/// - Katalog modülünde olduğu gibi, servis ve endpoint kayıtları burada merkezi olarak yönetilir.
/// </summary>
public static class IncidentModule
{
    /// <summary>
    /// Incident endpointlerini Carter ile modüler olarak ekler.
    /// </summary>
    public class Endpoints : CarterModule
    {
        public Endpoints() : base("incidents") { }
        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            var incident = app.MapGroup("incidents").WithTags("incidents");
            incident.MapIncidentEndpoints();
        }
    }

    /// <summary>
    /// Incident ile ilgili servisleri DI container'a ekler.
    /// </summary>
    public static WebApplicationBuilder RegisterIncidentServices(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        builder.Services.BindDbContext<IncidentDbContext>();
        builder.Services.AddKeyedScoped<IRepository<Incident.Domain.Incident>, IncidentRepository<Incident.Domain.Incident>>("incident");
        return builder;
    }

    /// <summary>
    /// Incident modülünü uygulamaya ekler (middleware, endpoint, vs.).
    /// </summary>
    public static WebApplication UseIncidentModule(this WebApplication app)
    {
        return app;
    }
}