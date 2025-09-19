using AppointmentReservation.Domain.Common;
using ErrorOr;

namespace AppointmentReservation.Domain.AppointmentAggregate.Events;

public record AppointmentCanceledEvent(Appointment Appointment) : IDomainEvent
{
	public static readonly Error PatientNotFound = Error.Conflict(
		"ScheduledAppointmentCanceledEvent.PatientNotFound",
		"Clinic not found"
		);

	public static readonly Error RoomNotFound = Error.Conflict(
	"ScheduledAppointmentCanceledEvent.RoomNotFound",
	"Room not found"
	);

	public static readonly Error UpdatePatientScheduleFailed = Error.Conflict(
		"ScheduledAppointmentCanceledEvent.UpdatePatientScheduleFailed",
		"Update patient schedule failed"
	);

	public static readonly Error DoctorNotFound = Error.Conflict(
		"ScheduledAppointmentCanceledEvent.DoctorNotFound",
		"doctor not found"
		);

	public static readonly Error UpdateDoctorScheduleFailed = Error.Conflict(
		"ScheduledAppointmentCanceledEvent.UpdateDoctorScheduleFailed",
		"Update doctor schedule failed"
	);

	public static readonly Error ClinicNotFound = Error.Conflict(
		"ScheduledAppointmentCanceledEvent.ClinicNotFound",
		"Clinic not found"
		);

	public static readonly Error UpdateRoomScheduleFailed = Error.Conflict(
		"ScheduledAppointmentCanceledEvent.UpdateRoomScheduleFailed",
		"Update room schedule failed"
	);
}