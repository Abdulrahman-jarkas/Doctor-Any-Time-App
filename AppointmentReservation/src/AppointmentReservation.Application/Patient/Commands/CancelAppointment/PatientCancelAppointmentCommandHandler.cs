using AppointmentReservation.Application.Common.Interfaces;
using AppointmentReservation.Domain.AppointmentAggregate;
using ErrorOr;
using MediatR;

namespace AppointmentReservation.Application.Patient.Commands.CancelAppointment;

public class PatientCancelAppointmentCommandHandler : IRequestHandler<PatientCancelAppointmentCommand, ErrorOr<Success>>
{
	private readonly IAppointmentRepository _appointmentRepository;

	public PatientCancelAppointmentCommandHandler(IAppointmentRepository appointmentRepository)
	{
		_appointmentRepository = appointmentRepository;
	}

	public async Task<ErrorOr<Success>> Handle(PatientCancelAppointmentCommand request, CancellationToken cancellationToken)
	{
		var appointment = await _appointmentRepository.Get(request.AppointmentId, AppointmentStatus.Scheduled);

		if (appointment == null || appointment.PatientId != request.PatientId)
			return Error.NotFound("Appointment.NotFound", "Appointment not found");

		var cancelRes = appointment.Cancel();

		if (cancelRes.IsError)
			return cancelRes;

		await _appointmentRepository.UpdateAsync(appointment);

		return Result.Success;
	}
}