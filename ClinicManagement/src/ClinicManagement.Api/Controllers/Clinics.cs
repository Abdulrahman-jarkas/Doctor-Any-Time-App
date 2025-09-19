using ClinicManagement.Application.Commands.AddClinic;
using ClinicManagement.Application.Commands.AddDoctor;
using ClinicManagement.Application.Commands.AddRoom;
using ClinicManagement.Application.Commands.ChangeSubscription;
using ClinicManagement.Application.Commands.RemoveClinic;
using ClinicManagement.Application.Commands.RemoveDoctor;
using ClinicManagement.Application.Commands.RemoveRoom;
using ClinicManagement.Domain.ClinicCenterAggregate.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagement.Api.Controllers
{
	public class ClinicsController(IMediator mediator) : ApiController
	{
		[HttpPost("clinics/add")]
		public async Task<IActionResult> AddClinic([FromBody] AddClinicRequest request)
		{
			var res = await mediator.Send(new AddClinicCommand(request.ClinicCenterId));

			return res.Match(res => Ok(), Problem);
		}

		[HttpDelete("clinics/{id:guid}/remove")]
		public async Task<IActionResult> RemoveClinic([FromBody] RemoveClinicRequest request, Guid id)
		{
			var res = await mediator.Send(new RemoveClinicCommand(request.ClinicCenterId, id));

			return res.Match(res => Ok(), Problem);
		}

		[HttpPost("clinics/{id:guid}/doctors/add")]
		public async Task<IActionResult> AddDoctor([FromBody] AddDoctorRequest request, Guid id)
		{
			var res = await mediator.Send(new AddDoctorCommand(id, request.ServiceIds));

			return res.Match(res => Ok(), Problem);
		}

		[HttpDelete("clinics/{id:guid}/doctors/{doctorId:guid}/remove")]
		public async Task<IActionResult> RemoveDoctor(Guid id, Guid doctorId)
		{
			var res = await mediator.Send(new RemoveDoctorCommand(id, doctorId));

			return res.Match(res => Ok(), Problem);
		}

		[HttpPost("clinics/{id:guid}/rooms/add")]
		public async Task<IActionResult> AddRoom([FromBody] AddRoomRequest request, Guid id)
		{
			var res = await mediator.Send(new AddRoomCommand(id, request.ServiceIds));

			return res.Match(res => Ok(), Problem);
		}

		[HttpDelete("clinics/{id:guid}/rooms/{roomId:guid}/remove")]
		public async Task<IActionResult> RemoveRoom(Guid id, Guid roomId)
		{
			var res = await mediator.Send(new RemoveRoomCommand(id, roomId));

			return res.Match(res => Ok(), Problem);
		}

		[HttpPut("change-subscription")]
		public async Task<IActionResult> ChangeSubscription([FromBody] ChangeSubscriptionRequest request)
		{
			var res = await mediator.Send(new ChangeSubscriptionCommand(request.ClinicCenterId, request.SubscriptionType));

			return res.Match(res => Ok(), Problem);
		}
	}
}

public class AddClinicRequest
{
	public Guid ClinicCenterId { get; set; }
}

public class RemoveClinicRequest
{
	public Guid ClinicCenterId { get; set; }
}

public class AddDoctorRequest
{
	public List<Guid> ServiceIds { get; set; } = new();
}

public class AddRoomRequest
{
	public List<Guid> ServiceIds { get; set; } = new();
}

public class ChangeSubscriptionRequest
{
	public SubscriptionType SubscriptionType { get; set; }
	public Guid ClinicCenterId { get; set; }
}