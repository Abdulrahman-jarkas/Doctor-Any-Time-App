using AppointmentReservation.Application.Common.Interfaces;
using AppointmentReservation.Domain.Common.Entities;
using AppointmentReservation.Domain.PatientAggregate;

namespace AppointmentReservation.Infrastructure.Persistence.Configuration;

public static class SeedData
{
	private static readonly Guid patientId = Guid.Parse("c1d2b349-77f1-425d-a9f5-7137002c0d95");
	private static readonly Schedule emptySchedule = Schedule.Empty();

	public static async Task InitializeAsync(IPatientRepository patientRepository)
	{

		// Avoid reseeding if data already exists
		if (await patientRepository.AnyAsync())
			return;

		var patient = new Patient(emptySchedule, patientId);

		await patientRepository.AddAsync(patient);
	}
}