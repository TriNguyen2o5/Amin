﻿using System;
using System.Collections.Generic;

namespace Amin.Data;

public partial class Comment
{
    public int CommentId { get; set; }

    public int PostId { get; set; }

    public string CommentContent { get; set; } = null!;

    public int CommentAuthorId { get; set; }

    public DateTime CommentDate { get; set; }

    public virtual User CommentAuthor { get; set; } = null!;

    public virtual Post Post { get; set; } = null!;
}
