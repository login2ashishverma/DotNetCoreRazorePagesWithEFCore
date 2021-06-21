using DotnetCoreRazorWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotnetCoreRazorWebApp.Services
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            :base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
    }
}
