using System;
using System.Collections.Generic;

namespace Amin.Models;

public partial class PatientInformation
{
    public int RecordId { get; set; }

    public int PhysicalActivityDuration { get; set; }

    public string CaffeineIntake { get; set; } = null!;

    public int SleepTime { get; set; }

    public int WakeTime { get; set; }

    public DateOnly Date { get; set; }
}
