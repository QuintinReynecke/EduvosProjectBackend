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

        public DbSet<FAQTable> FAQTable { get; set; }

        public DbSet<GroupMessageTable> GroupMessageTable { get; set; }

        public DbSet<PersonalChatsTable> PersonalChatsTable { get; set; }

        public DbSet<DepartmentTable> DepartmentTable { get; set; }

        public DbSet<SubjectsTable> SubjectsTable { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.Migrate();
        }

    }

}

