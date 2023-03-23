

namespace Infraestructure.Models
{
    using System;
    using Microsoft.EntityFrameworkCore;
   
    using Transversals.Connections;

    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public virtual DbSet<UserInfo> UserInfo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
     => optionsBuilder.UseSqlServer(ConnectionsManager.Instance.DatabaseConnection);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {           
           

            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.ToTable("UserInfo");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Name");
                entity.Property(e => e.Birthdate)
                    .HasColumnType("datetime")
                    .IsUnicode(false)
                    .HasColumnName("Birthdate");
                entity.Property(e => e.Sex)                   
                    .IsUnicode(false)                    
                    .HasColumnName("Sex");
               
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
