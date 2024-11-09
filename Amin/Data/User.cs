using System;
using System.Collections.Generic;

namespace Amin.Data;

public partial class User
{
    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public int? Age { get; set; }

    public DateOnly? YearOfBirth { get; set; }

    public string? Address { get; set; }

    public int Gender { get; set; }

    public decimal? Bmi { get; set; }

    public string Password { get; set; } = null!;

    public bool? SmokingStatus { get; set; }

    public bool? AlcoholicStatus { get; set; }

    public string Username { get; set; } = null!;

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<PatientInformation> PatientInformations { get; set; } = new List<PatientInformation>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
