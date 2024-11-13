using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Amin.Data;

public partial class PatientInformation
{
    public int RecordId { get; set; }

    public DateTime? Date { get; set; }

    public int? PhysicalActivityDuration { get; set; }

    public decimal? CaffeineIntake { get; set; }

    public TimeSpan? SleepTime { get; set; }

    public TimeSpan? WakeTime { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
