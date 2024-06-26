﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Log.Core.JWT;

public class ApiSecurityDbContext : IdentityDbContext<ApplicationUser>
{
    public ApiSecurityDbContext(DbContextOptions<ApiSecurityDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}