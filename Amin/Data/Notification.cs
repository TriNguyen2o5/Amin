using System;
using System.Collections.Generic;

namespace Amin.Data;

public partial class Notification
{
    public int NotificateId { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }

    public DateOnly? SendDay { get; set; }
}
