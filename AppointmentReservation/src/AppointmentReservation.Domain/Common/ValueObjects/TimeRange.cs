using AppointmentReservation.Core.Common;
using ErrorOr;
using Throw;

namespace AppointmentReservation.Domain.Common.ValueObjects;

public class TimeRange : ValueObject
{
    public TimeOnly Start { get; init; }
    public TimeOnly End { get; init; }

    private TimeRange(TimeOnly start, TimeOnly end)
    {
        Start = start.Throw().IfGreaterThanOrEqualTo(end);
        End = end;
    }

    public static ErrorOr<TimeRange> FromTimes(TimeOnly start, TimeOnly end)
    {
        if (start >= end)
            return Error.Validation();

        return new TimeRange(start, end);
    }

    public bool OverlapsWith(TimeRange other)
    {
        if (Start >= other.End) return false;
        if (other.Start >= End) return false;

        return true;
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Start;
        yield return End;
    }

	public TimeRange() { } // Required for parameterless deserialization
}