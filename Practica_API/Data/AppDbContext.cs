using Microsoft.EntityFrameworkCore;
using Practica_API.Models;

namespace Practica_API.Data
{
    public partial class AppDbContext : DbContext
    {

        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Beers> Beers { get; set; }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Beer>().HasData(
        //        new Beer()
        //        {
        //            Id = 1,
        //            Name = "tecate",
        //            Description = "cerveza de botella",
        //            Amount = 50,
        //            Price = 100,
        //            CreatedAt = DateTime.Now,
        //            UpdatedAt = DateTime.Now







        //        });
        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Name = defaultConnectionDev");

        /* protected override void OnModelCreating(ModelBuilder modelBuilder)
         {
     *//*        modelBuilder.Entity<TagsMapping>(entity =>
             {
                 entity.HasKey(e => e.LocalId).HasName("PK__TagsMa__499359BB8862A0E9");

                 entity.ToTable("TagsMapping");

                 entity.Property(e => e.MappedTag)
                     .HasMaxLength(100)
                     .IsUnicode(false);
                 entity.Property(e => e.OriginalTag)
                     .HasMaxLength(50)
                     .IsUnicode(false);
             });*/
        /*
        modelBuilder.Entity<OrganizationDataSpecif>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("OrganizationDataSpecif");

            entity.Property(e => e.ClientId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ClientID");
            entity.Property(e => e.ClientSecret)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ExtractSpecificMonthDate)
                .HasMaxLength(7)
                .IsUnicode(false);
            entity.Property(e => e.OperationMode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TenantId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TenantID");
        });

        modelBuilder.Entity<OrganizationDatum>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.OrgDataField)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.OrgDataRowId).ValueGeneratedOnAdd();
            entity.Property(e => e.OrgDataValue)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC095E9087");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534E13D186D").IsUnique();

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("UserID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
        });
        */
        /*        modelBuilder.Entity<WhichTagAreThere>(entity =>
                {
                    entity.HasKey(e => e.Id).HasName("PK__whichTag__3214EC07E231107C");

                    entity.ToTable("whichTagAreThere");

                    entity.Property(e => e.ResourceGroup)
                        .HasMaxLength(100)
                        .IsUnicode(false);
                    entity.Property(e => e.SubscriptionName)
                        .HasMaxLength(100)
                        .IsUnicode(false);
                    entity.Property(e => e.TagName)
                        .HasMaxLength(100)
                        .IsUnicode(false);
                });*//*

        //OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);*/

        //public DbSet<PortalC2MAPI.Models.TagsMapping>? TagsMapping { get; set; }
    }
}
