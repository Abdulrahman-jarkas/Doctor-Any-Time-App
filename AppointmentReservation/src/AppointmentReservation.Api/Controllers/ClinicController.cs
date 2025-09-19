using AppointmentReservation.Application.Clinic.Commands.AcceptAppointmentRequest;
using AppointmentReservation.Application.Clinic.Commands.PublishAvailability;
using AppointmentReservation.Application.Patient.Commands.CancelAppointment;
using AppointmentReservation.Application.Patient.Commands.CancelAvailabilityRequest;
using AppointmentReservation.Application.Patient.Commands.RequestAvailability;
using AppointmentReservation.Contracts.Clinic;
using AppointmentReservation.Contracts.Patient;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentReservation.Api.Controllers;

public class ClinicController : ApiController
{
	private readonly IMediator _mediator;

	public ClinicController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost("clinics/{clinicId:guid}/publish-availability")]
	public async Task<IActionResult> PublishAvailability([FromBody] PublishAvailabilityRequest request, Guid clinicId)
	{
		var command = new PublishAvailabilityCommand(
			clinicId,
			request.DoctorId,
			request.ServiceIds,
			request.AppointmentDate,
			request.StartTime,
			request.EndTime);

		var res = await _mediator.Send(command);

		return res.Match(appointment => Ok(res), Problem);
	}

	[HttpPost("availabilities/{id:guid}/request")]
	public async Task<IActionResult> RequestAvailability([FromBody] RequestAppointmentRequest request, Guid id)
	{
		var command = new RequestAvailabilityCommand(request.PatientId, id);

		var res = await _mediator.Send(command);

		return res.Match(res => Ok(), Problem);
	}

	[HttpPut("availabilities/{id:guid}/requests/{requestId:guid}/accept")]
	public async Task<IActionResult> AcceptAvailabilityRequest(
		Guid id,
		Guid requestId)
	{
		var command = new AcceptAvailabilityRequestCommand(id, requestId);

		var res = await _mediator.Send(command);

		return res.Match(r => Ok(), Problem);
	}

	[HttpPut("availabilities/{id:guid}/requests/{requestId:guid}/cancel")]
	public async Task<IActionResult> CancelAvailabilityRequest(
		Guid id,
		Guid requestId, [FromBody] CancelAppointmentRequest request)
	{
		var command = new CancelAvailabilityRequestCommand(id, requestId);

		var res = await _mediator.Send(command);

		return res.Match(r => Ok(), Problem);
	}

	

	[HttpPut("patients/{id:guid}/appointments/{appointmentId:guid}/cancel")]
	public async Task<IActionResult> CancelAppointment(Guid id, Guid appointmentId)
	{
		var command = new PatientCancelAppointmentCommand(appointmentId, id);

		var res = await _mediator.Send(command);

		return res.Match(res => Ok(), Problem);
	}
}