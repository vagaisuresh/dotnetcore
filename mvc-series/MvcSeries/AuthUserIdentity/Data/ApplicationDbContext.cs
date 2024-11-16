﻿using AuthUserIdentity.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Userrolemaster> Userrolemasters { get; set; } = default!;
    public DbSet<User> Users { get; set; } = default!;
}