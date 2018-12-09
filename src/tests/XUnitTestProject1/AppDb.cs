using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using XUnitTestProject1.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace XUnitTestProject1
{
	public class AppDb : IdentityDbContext<AppIdentityUser>
	{
		// sequence
		public DbSet<Sequence> Sequence { get; set; }
		public DbSet<Users> Users { get; set; }


		public AppDb(DbContextOptions options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Shorten key length for Identity
			modelBuilder.Entity<AppIdentityUser>(entity => {
				entity.Property(m => m.Email).HasMaxLength(127);
				entity.Property(m => m.NormalizedEmail).HasMaxLength(127);
				entity.Property(m => m.NormalizedUserName).HasMaxLength(127);
				entity.Property(m => m.UserName).HasMaxLength(127);
			});
			modelBuilder.Entity<IdentityRole>(entity => {
				entity.Property(m => m.Name).HasMaxLength(127);
				entity.Property(m => m.NormalizedName).HasMaxLength(127);
			});
			modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
			{
				entity.Property(m => m.LoginProvider).HasMaxLength(127);
				entity.Property(m => m.ProviderKey).HasMaxLength(127);
			});
			modelBuilder.Entity<IdentityUserRole<string>>(entity =>
			{
				entity.Property(m => m.UserId).HasMaxLength(127);
				entity.Property(m => m.RoleId).HasMaxLength(127);
			});
			modelBuilder.Entity<IdentityUserToken<string>>(entity =>
			{
				entity.Property(m => m.UserId).HasMaxLength(127);
				entity.Property(m => m.LoginProvider).HasMaxLength(127);
				entity.Property(m => m.Name).HasMaxLength(127);

			});

			if (AppConfig.EfSchema != null)
			{
				// Generates all models in a different schema
				modelBuilder.HasDefaultSchema(AppConfig.EfSchema);
			}

		}
	}
}
