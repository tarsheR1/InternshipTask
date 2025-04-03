namespace TicketManagementService.Domain.Common
{
    public abstract class BaseEntity<TKey>
    {
        public TKey Id { get; protected set; }
        private List<DomainEvent> _domainEvents = new();
        public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        protected void AddDomainEvent(DomainEvent eventItem) => _domainEvents.Add(eventItem);
        public void ClearDomainEvents() => _domainEvents.Clear();

        public override bool Equals(object obj)
        {
            if (obj is not BaseEntity<TKey> other)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Id.Equals(other.Id);
        }

        public override int GetHashCode() => HashCode.Combine(Id);
    }
}
