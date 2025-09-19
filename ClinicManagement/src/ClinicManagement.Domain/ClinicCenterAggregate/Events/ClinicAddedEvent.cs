using ClinicManagement.Domain.Common;
using ClinicManagement.Domain.ClinicAggregate;

namespace ClinicManagement.Domain.ClinicCenterAggregate.Events;

public record ClinicAddedEvent(Clinic Clinic, int MaxAppointmentPerDay) : IDomainEvent;
