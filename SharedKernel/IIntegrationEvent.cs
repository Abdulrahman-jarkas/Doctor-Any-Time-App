using MediatR;
using SharedKernel.AppointmentReservation;
using SharedKernel.ClinicManagement;
using System.Text.Json.Serialization;
namespace SharedKernel;

[JsonDerivedType(typeof(ClinicAddedIntegrationEvent), typeDiscriminator: nameof(ClinicAddedIntegrationEvent))]
[JsonDerivedType(typeof(ClinicRemovedIntegrationEvent), typeDiscriminator: nameof(ClinicRemovedIntegrationEvent))]
[JsonDerivedType(typeof(RoomAddedIntegrationEvent), typeDiscriminator: nameof(RoomAddedIntegrationEvent))]
[JsonDerivedType(typeof(RoomRemovedIntegrationEvent), typeDiscriminator: nameof(RoomRemovedIntegrationEvent))]
[JsonDerivedType(typeof(DoctorAddedIntegrationEvent), typeDiscriminator: nameof(DoctorAddedIntegrationEvent))]
[JsonDerivedType(typeof(DoctorRemovedIntegrationEvent), typeDiscriminator: nameof(DoctorRemovedIntegrationEvent))]
[JsonDerivedType(typeof(SubscriptionChangedIntegrationEvent), typeDiscriminator: nameof(SubscriptionChangedIntegrationEvent))]
[JsonDerivedType(typeof(AppointmentCreatedIntegrationEvent), typeDiscriminator: nameof(AppointmentCreatedIntegrationEvent))]
[JsonDerivedType(typeof(AppointmentCanceledIntegrationEvent), typeDiscriminator: nameof(AppointmentCanceledIntegrationEvent))]
[JsonDerivedType(typeof(AppointmentCompletedIntegrationEvent), typeDiscriminator: nameof(AppointmentCompletedIntegrationEvent))]
public interface IIntegrationEvent : INotification;