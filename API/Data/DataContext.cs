<<<<<<< HEAD
﻿using System;
using System.Data.Common;
using API.Entities;
=======
﻿using API.Entities;
>>>>>>> 4d6ede2e86e4e28fd14b486ac9d1ad4de161446a
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {

    }
    public DbSet<AppUser> Users { get; set; }
<<<<<<< HEAD
}
=======
}
>>>>>>> 4d6ede2e86e4e28fd14b486ac9d1ad4de161446a
