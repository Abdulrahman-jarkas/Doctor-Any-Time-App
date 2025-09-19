using ClinicManagement.Domain.Common;
using ClinicManagement.Domain.DoctorAggregate;

namespace ClinicManagement.Domain.ClinicAggregate.Events;

public record DoctorAddedEvent(Doctor doctor) : IDomainEvent;
