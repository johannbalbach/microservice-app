﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using User.Domain.Entities;

namespace User.Domain.Context
{
    public class AppDbContext: IdentityDbContext<UserE>
    {
        public DbSet<Token> Tokens { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
            //Database.EnsureCreated();
        }
    }
}
