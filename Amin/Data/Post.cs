using System;
using System.Collections.Generic;

namespace Amin.Data;

public partial class Post
{
    public int PostId { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }

    public string? AuthorName { get; set; }

    public DateTime? PostedDate { get; set; }

    public string? PostImageId { get; set; }

    public string? PostImageData { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
