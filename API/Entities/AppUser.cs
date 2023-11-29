<<<<<<< HEAD
﻿using System;

namespace API.Entities;

public class AppUser
{
    //snippet: typing "prop" then press tap
    public int Id { get; set; }
    public string? UserName { get; set; }

    public required byte[] PasswordHash { get; set; }
    public required byte[] PasswordSalt { get; set; }

=======
﻿namespace API.Entities;

#nullable disable
public class AppUser
{
    public int Id { get; set; }
    public string UserName { get; set; }
>>>>>>> 4d6ede2e86e4e28fd14b486ac9d1ad4de161446a
}
