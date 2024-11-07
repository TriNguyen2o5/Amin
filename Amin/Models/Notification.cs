using System;
using System.Collections.Generic;

namespace Amin.Models;

public partial class Notification
{
    public int NotificateId { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public DateOnly SendDay { get; set; }
}
