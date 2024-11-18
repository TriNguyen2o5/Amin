using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Amin.Data;

public partial class Comment
{
    [Key]
    public int CommentId { get; set; }

    public int PostId { get; set; }
    [Required(ErrorMessage = "Yêu cầu nhập nhập nội dung")]
    public string? CommentContent { get; set; }
    
    public int? CommentAuthorId { get; set; }

    public DateTime? CommentDate { get; set; }

    public virtual User? CommentAuthor { get; set; }

    public virtual Post Post { get; set; } = null!;
}
