using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Amin.Data;

public partial class Post
{
    [Key]
    public int PostId { get; set; }
    [Required(ErrorMessage = "Yêu cầu nhập tiêu đề")]
    public string? Title { get; set; }
    [Required(ErrorMessage = "Yêu cầu nhập nội dung")]
    public string? Content { get; set; }
    [Required(ErrorMessage = "Yêu cầu nhập tên tác giả")]
    public string? AuthorName { get; set; }
    [Required(ErrorMessage = "Yêu cầu nhập ngày đăng")]
    public DateTime? PostedDate { get; set; }

    public string? PostImageId { get; set; }

    public string? PostImageData { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
