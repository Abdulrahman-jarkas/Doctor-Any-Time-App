using ClinicManagement.Domain.Common;
using ClinicManagement.Domain.ClinicCenterAggregate.ValueObjects;

namespace ClinicManagement.Domain.ClinicCenterAggregate.Events;

public record SubscriptionChangedEvent(Guid ClinicCenterId, Subscription Subscription) : IDomainEvent;