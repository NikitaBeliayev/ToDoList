﻿using Shared;

namespace Domain.Users;

public static class NameErrors
{
    public static Error NullOrEmpty =>
        new("Users.Name.NullOrEmpty", $"The Name value cannot be NullOrEmpty", 422);
}
