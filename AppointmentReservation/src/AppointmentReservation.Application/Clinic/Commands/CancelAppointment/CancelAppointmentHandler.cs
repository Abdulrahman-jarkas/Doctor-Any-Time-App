using AppointmentReservation.Application.Common.Interfaces;
using AppointmentReservation.Domain.AppointmentAggregate;
using ErrorOr;
using MediatR;

namespace AppointmentReservation.Application.Clinic.Commands.CancelAppointment;

public class CancelAppointmentHandler : IRequestHandler<CancelAppointment, ErrorOr<Success>>
{
	private readonly IAppointmentRepository _appointmentRepository;

	public CancelAppointmentHandler(
		IAppointmentRepository appointmentRepository
		)
	{
		_appointmentRepository = appointmentRepository;
	}

	public async Task<ErrorOr<Success>> Handle(CancelAppointment request, CancellationToken cancellationToken)
	{
		var appointment = await _appointmentRepository.Get(request.AppointmentId, AppointmentStatus.Scheduled);

		if (appointment == null || appointment.ClinicId != request.ClinicId)
			return Error.NotFound("Appointemnt", "Appointemnt not found");

		var cancelRes = appointment.Cancel();

		if (cancelRes.IsError)
			return cancelRes;

		await _appointmentRepository.UpdateAsync(appointment);

		return Result.Success;
	}
}
