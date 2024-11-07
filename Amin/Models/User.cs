using System;
using System.Collections.Generic;

namespace Amin.Models;

public partial class User
{
    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public DateOnly YearOfBirth { get; set; }

    public string Address { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public int Bmi { get; set; }

    public string Password { get; set; } = null!;

    public string SmokingStatus { get; set; } = null!;

    public string AlcoholicStatus { get; set; } = null!;

    public string Username { get; set; } = null!;
}
