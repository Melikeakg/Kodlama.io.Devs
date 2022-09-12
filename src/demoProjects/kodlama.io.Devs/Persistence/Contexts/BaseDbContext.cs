﻿using Core.Security.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistence.Contexts
{
    public class BaseDbContext : DbContext
    {
        protected IConfiguration Configuration { get; set; }
        public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<UserSocialMedia> UserSocialMedias { get; set; }




        public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //    base.OnConfiguring(
            //        optionsBuilder.UseSqlServer(Configuration.GetConnectionString("SomeConnectionString")));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Microsoft.EntityFrameworkCore.SqlServer paketi kurulmazsa hata verir.
            modelBuilder.Entity<ProgrammingLanguage>(a =>
            {
                a.ToTable("ProgrammingLanguages").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.Name).HasColumnName("Name");
                a.HasMany(p => p.Technologies);
            });

            modelBuilder.Entity<Technology>(a =>
            {
                a.ToTable("Technologies").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.Name).HasColumnName("Name");
                a.Property(p => p.ProgrammingLanguageId).HasColumnName("ProgrammingLanguageId");
                a.HasOne(p => p.ProgrammingLanguage);
            });

            modelBuilder.Entity<User>(a =>
            {
                a.ToTable("Users").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.FirstName).HasColumnName("FirstName");
                a.Property(p => p.LastName).HasColumnName("LastName");
                a.Property(p => p.Email).HasColumnName("Email");
                a.Property(c => c.PasswordSalt).HasColumnName("PasswordSalt");
                a.Property(c => c.PasswordHash).HasColumnName("PasswordHash");
                a.Property(c => c.Status).HasColumnName("Status");
                a.Property(c => c.AuthenticatorType).HasColumnName("AuthenticatorType");

                a.HasMany(c => c.UserOperationClaims);
                a.HasMany(c => c.RefreshTokens);

            });

            modelBuilder.Entity<OperationClaim>(a =>
            {
                a.ToTable("OperationClaims").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(c => c.Name).HasColumnName("Name");
            });

            modelBuilder.Entity<UserOperationClaim>(a =>
            {
                a.ToTable("UserOperationClaims").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(c => c.UserId).HasColumnName("UserId");
                a.Property(c => c.OperationClaimId).HasColumnName("OperationClaimId");

                a.HasOne(c => c.OperationClaim);
                a.HasOne(c => c.User);
            });

            modelBuilder.Entity<UserSocialMedia>(e =>
            {
                e.ToTable("UserWebAddresses").HasKey(k => k.Id);
                e.Property(u => u.Id).HasColumnName("Id");
                e.Property(u => u.UserId).HasColumnName("UserId");
                e.Property(u => u.SocialMediaLink).HasColumnName("SocialMediaLink");
                e.HasOne(u => u.User);
            });



            ProgrammingLanguage[] programingLanguageEntitySeeds = { new(1, "C#"), new(2, "SQL") };
            modelBuilder.Entity<ProgrammingLanguage>().HasData(programingLanguageEntitySeeds);

            Technology[] technologyEntitySeeds = { new(1, "ASP.NET",1)};
            modelBuilder.Entity<Technology>().HasData(technologyEntitySeeds);


        }
    }
}
