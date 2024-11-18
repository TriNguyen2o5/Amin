using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Amin.Data;

public partial class PatientInformation
{
    [Key]
    public int RecordId { get; set; }
    [Required(ErrorMessage = "Yêu cầu chọn ngày")]
    public DateTime? Date { get; set; }
    [Required(ErrorMessage = "Yêu cầu cho biết lượng mức độ hoạt động thể chất")]
    public int? PhysicalActivityDuration { get; set; }
    [Required(ErrorMessage = "Yêu cầu cho biết lượng mức độ sử dụng caffeine")]
    public decimal? CaffeineIntake { get; set; }
    [Required(ErrorMessage = "Yêu cầu cho biết giờ ngủ")]
    public TimeSpan? SleepTime { get; set; }
    [Required(ErrorMessage = "Yêu cầu cho biết giờ thức dậy")]
    public TimeSpan? WakeTime { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
