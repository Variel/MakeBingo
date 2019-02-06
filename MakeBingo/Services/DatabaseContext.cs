using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MakeBingo.Models;
using Microsoft.EntityFrameworkCore;

namespace MakeBingo.Services
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Board> Boards { get; set; }
        public DbSet<Result> Results { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Board>()
                .Property<string>("CellsStr")
                .HasField("_cells");

            builder.Entity<Result>()
                .Property<string>("CellsStr")
                .HasField("_cells");

            builder.Entity<Board>()
                .HasMany(b => b.Children)
                .WithOne(c => c.Parent)
                .HasForeignKey(c => c.ParentId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            base.OnModelCreating(builder);
        }
    }
}
