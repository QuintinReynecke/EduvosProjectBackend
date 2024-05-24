using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Helpers
{
    public class DataContext : DbContext
    {
        // Update Database with:
        // Remember to save all first (Dot net is fragile)
        // dotnet ef migrations add <InitialCreate>
        // dotnet ef database update

        public DbSet<MainTable> MainTable { get; set; }

        public DbSet<BusinessHoursTable> BusinessHoursTable { get; set; }

        public DbSet<ContactDetailsTable> ContactDetailsTable { get; set; }

        public DbSet<PhotosTable> PhotosTable { get; set; }

        public DbSet<WorkLocationTable> WorkLocationTable { get; set; }

        public DbSet<UserTable> UserTable { get; set; }

        public DbSet<ReviewTable> ReviewTable { get; set; }

        public DbSet<ServiceList> ServiceList { get; set; }

        public DbSet<JobRequest> JobRequests { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.Migrate();
        }

    }

}

