using ClinicManagement.Domain.ClinicCenterAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace ClinicManagement.Application.Commands.ChangeSubscription;

public record ChangeSubscriptionCommand(Guid ClinicCenterId, SubscriptionType SubscriptionType) : IRequest<ErrorOr<Success>>;
