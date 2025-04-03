using TicketManagementService.Domain.Entities;
using TicketManagementService.Domain.Interfaces.Repositories;

namespace TicketManagementService.Domain.Services;

public class TicketReservationService
{
    private readonly ITicketInventoryRepository _inventoryRepository;
    private readonly ITicketRepository _ticketRepository;

    public TicketReservationService(
        ITicketInventoryRepository inventoryRepository,
        ITicketRepository ticketRepository)
    {
        _inventoryRepository = inventoryRepository;
        _ticketRepository = ticketRepository;
    }

    public async Task<Ticket> ReserveTicketAsync(
        Guid eventId,
        Guid userId,
        string ticketType,
        int quantity,
        CancellationToken cancellationToken)
    {
        var inventory = await _inventoryRepository.GetByEventAndTypeAsync(
            eventId, 
            ticketType, 
            cancellationToken);

        if (inventory == null)
            throw new ArgumentException("Wrong type of tickets.");

        inventory.ReserveTickets(quantity);
        await _inventoryRepository.UpdateInventoryAsync(inventory, cancellationToken);

        var ticket = new Ticket(
            eventId,
            userId,
            ticketType,
            DateTime.UtcNow,
            DateTime.UtcNow.AddMinutes(15));

        await _ticketRepository.AddAsync(ticket, cancellationToken);
        return ticket;
    }
}