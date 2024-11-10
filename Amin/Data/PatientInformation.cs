using System;
using System.Collections.Generic;

namespace Amin.Data;

public partial class PatientInformation
{
    public int RecordId { get; set; }

    public DateOnly? Date { get; set; }

    public int? PhysicalActivityDuration { get; set; }

    public decimal? CaffeineIntake { get; set; }

    public TimeOnly? SleepTime { get; set; }

    public TimeOnly? WakeTime { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
