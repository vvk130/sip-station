﻿using System;
using System.Collections.Generic;

namespace SipStation;

public partial class User
{
    public int Id { get; set; }

    public string? PasswordHash { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;
}
