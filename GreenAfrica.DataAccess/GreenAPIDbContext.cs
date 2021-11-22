﻿using GreenAfrica.DataAccess.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace GreenAfrica.DataAccess
{
    public class GreenAPIDbContext : IdentityDbContext
    {
        public GreenAPIDbContext(DbContextOptions<GreenAPIDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CPayroll> Cpayroll { get; set; }

    }
}