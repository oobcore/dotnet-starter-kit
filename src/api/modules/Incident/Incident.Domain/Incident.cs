using FSH.Framework.Core.Domain;
using FSH.Framework.Core.Domain.Contracts;

namespace Incident.Domain
{
  /// <summary>
  /// Incident entity'si, bir olay kaydını temsil eder.
  /// Factory Method Pattern ve kapsülleme (encapsulation) uygulanmıştır.
  /// Nesne oluşturma işlemi sadece Create metodu ile yapılabilir, constructor gizlidir.
  /// Bu sayede domain kuralları ve tutarlılık korunur.
  /// </summary>
  public class Incident : AuditableEntity, IAggregateRoot
  {

    private Incident() { }

    private Incident(string ticketCode,
             string ticketSubject,
             string ticketDescription,
             string ticketDescriptionTextFormat,
             int? agentId,
             int? agentGroupId)
    {
      TicketCode = ticketCode;
      TicketSubject = ticketSubject;
      TicketDescription = ticketDescription;
      TicketDescriptionTextFormat = ticketDescriptionTextFormat;
      AgentId = agentId;
      AgentGroupId = agentGroupId;
    }

    public string TicketCode { get; private set; }
    public string TicketSubject { get; private set; }
    public string TicketDescription { get; private set; }
    public string TicketDescriptionTextFormat { get; private set; }
    public int? AgentId { get; private set; }
    public int? AgentGroupId { get; private set; }

    public static Incident Create(string ticketCode,
        string ticketSubject,
        string ticketDescription,
        string ticketDescriptionTextFormat,
        int? agentId,
        int? agentGroupId)
    {
      return new Incident(ticketCode, ticketSubject, ticketDescription, ticketDescriptionTextFormat, agentId, agentGroupId);
    }

    public void Update(
        string ticketCode,
        string ticketSubject,
        string ticketDescription,
        string ticketDescriptionTextFormat,
        int? agentId,
        int? agentGroupId)
    {
      TicketCode = ticketCode;
      TicketSubject = ticketSubject;
      TicketDescription = ticketDescription;
      TicketDescriptionTextFormat = ticketDescriptionTextFormat;
      AgentId = agentId;
      AgentGroupId = agentGroupId;
    }

    /// <summary>
    /// Sadece AgentGroupId alanını günceller.
    /// </summary>
    public void UpdateAgentGroupId(int? agentGroupId)
    {
        AgentGroupId = agentGroupId;
    }
  }
}

