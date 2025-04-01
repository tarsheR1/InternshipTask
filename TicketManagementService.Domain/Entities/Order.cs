using TicketManagementService.Domain.Common;

namespace TicketManagementService.Domain.Entities;

public class Order : BaseEntity
{
    public string UserId { get; }
    public IReadOnlyList<Ticket> Tickets => _tickets.AsReadOnly();
    public decimal TotalAmount { get; }
    public OrderStatus Status { get; private set; }
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; private set; }

    private readonly List<Ticket> _tickets = new();

    private Order() { } 

    public Order(string userId, IEnumerable<Ticket> tickets)
    {
        UserId = userId;
        _tickets.AddRange(tickets);
        TotalAmount = tickets.Sum(t => t.Price);
        Status = OrderStatus.Pending;
        CreatedAt = DateTime.UtcNow;
    }

    public void ConfirmPayment()
    {
        if (Status != OrderStatus.Pending)
            throw new DomainException("Заказ уже обработан");

        foreach (var ticket in _tickets)
            ticket.ConfirmPurchase();

        Status = OrderStatus.Completed;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Cancel()
    {
        if (Status != OrderStatus.Pending)
            throw new DomainException("Невозможно отменить завершенный заказ");

        foreach (var ticket in _tickets)
            ticket.CancelReservation();

        Status = OrderStatus.Cancelled;
        UpdatedAt = DateTime.UtcNow;
    }
}
