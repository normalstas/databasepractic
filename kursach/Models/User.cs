﻿using System;
using System.Collections.Generic;

namespace kursach.Models;

public partial class User
{
    public int UserId { get; set; }

    public string UserSurname { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public int UserPathronymic { get; set; }

    public string UserLogin { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public int UserRole { get; set; }

    public virtual Role UserRoleNavigation { get; set; } = null!;
}
