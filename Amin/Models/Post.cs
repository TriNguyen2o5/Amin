using System;
using System.Collections.Generic;

namespace Amin.Models;

public partial class Post
{
    public int PostId { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public int AuthorId { get; set; }

    public DateOnly PostedDate { get; set; }
}
