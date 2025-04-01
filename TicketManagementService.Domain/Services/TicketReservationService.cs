using TicketManagementService.Domain.Entities;
using TicketManagementService.Domain.Exceptions;
using TicketManagementService.Domain.Interfaces.Repositories;

namespace TicketManagementService.Domain.Services;

public class TicketReservationService
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IEventCacheRepository _eventCacheRepository;

    public TicketReservationService(
        ITicketRepository ticketRepository,
        IEventCacheRepository eventCacheRepository)
    {
        _ticketRepository = ticketRepository;
        _eventCacheRepository = eventCacheRepository;
    }

    public async Task<Order> ReserveTicketsAsync(
        string userId,
        string eventId,
        int ticketCount)
    {
        var @event = await _eventCacheRepository.GetByEventIdAsync(eventId);
        if (@event == null)
            throw new DomainException("Event do not exist");

        var availableTickets = await _ticketRepository.GetAvailableByEventIdAsync(eventId);
        if (availableTickets.Count < ticketCount)
            throw new DomainException("Not enough tickets");

        var ticketsToReserve = availableTickets.Take(ticketCount).ToList();
        foreach (var ticket in ticketsToReserve)
            ticket.Reserve(userId, Guid.NewGuid().ToString());

        return new Order(userId, ticketsToReserve);
    }
}