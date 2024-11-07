using System;
using System.Collections.Generic;

namespace Amin.Models;

public partial class Comment
{
    public int CommentId { get; set; }

    public int PostId { get; set; }

    public string CommentContent { get; set; } = null!;

    public int CommentAuthorId { get; set; }

    public DateOnly CommentDate { get; set; }
}
