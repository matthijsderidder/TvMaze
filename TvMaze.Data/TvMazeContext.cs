using Microsoft.EntityFrameworkCore;
using TvMaze.Data.Models;
using TvMaze.Data.Relations;

namespace TvMaze.Data
{
    public class TvMazeContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Show> Shows { get; set; }

        //public DbSet<ShowPerson> ShowPersons { get; set; }

        public TvMazeContext(DbContextOptions<TvMazeContext> options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlite("Filename=TvMaze.db");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Person>(e => {
                e.HasKey(e => e.Id);
                e.HasMany(p => p.Shows)
                .WithMany(p => p.Cast)
                .UsingEntity<ShowPerson>(
                    j => j
                        .HasOne(rel => rel.Show)
                        .WithMany(s => s.ShowPersons)
                        .HasForeignKey(sp => sp.ShowId),
                    j => j
                        .HasOne(rel => rel.Person)
                        .WithMany(p => p.ShowPersons)
                        .HasForeignKey(sp => sp.PersonId),
                    j =>
                    {
                        j.HasKey(rel => new { rel.ShowId, rel.PersonId });
                        j.ToTable("ShowPersons");
                    });
            });

            builder.Entity<Show>(e =>
            {
                e.HasKey(e => e.Id);
                e.HasMany(s => s.Cast)
                .WithMany(s => s.Shows)
                .UsingEntity<ShowPerson>(
                    j => j
                        .HasOne(rel => rel.Person)
                        .WithMany(p => p.ShowPersons)
                        .HasForeignKey(sp => sp.PersonId),
                    j => j
                        .HasOne(rel => rel.Show)
                        .WithMany(s => s.ShowPersons)
                        .HasForeignKey(sp => sp.ShowId),
                    j =>
                    {
                        j.HasKey(rel => new { rel.ShowId, rel.PersonId });
                        j.ToTable("ShowPersons");
                    });
            });
        }
    }
}
