using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Amin.Data;

public partial class Notification
{
    [Key]
    public int NotificateId { get; set; }
    [Required(ErrorMessage ="Yêu cầu nhập tiêu đề")]
    public string? Title { get; set; }
    [Required(ErrorMessage = "Yêu cầu nhập nội dung")]
    public string? Content { get; set; }
    [Required(ErrorMessage = "Yêu cầu nhập ngày đăng")]
    public DateOnly? SendDay { get; set; }
}
