using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using profile_api.domain.Entities;
using profile_api.domain.Entities.User;

namespace profile_api.domain
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<PostImage> PostImages { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<PostTag>()
                .HasKey(pt => new { pt.PostId, pt.TagId });

            builder.Entity<PostTag>()
                .HasOne(pt => pt.Post)
                .WithMany(p => p.PostTags)
                .HasForeignKey(pt => pt.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<PostTag>()
                .HasOne(pt => pt.Tag)
                .WithMany(t => t.PostTags)
                .HasForeignKey(pt => pt.TagId)
                .OnDelete(DeleteBehavior.Cascade);

            // 🔹 PostFile -> Post
            builder.Entity<PostImage>()
                .HasOne(pf => pf.Post)
                .WithMany(p => p.PostFiles)
                .HasForeignKey(pf => pf.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            // 🔹 PostFile -> User (Uploader)
            builder.Entity<PostImage>()
                .HasOne(pf => pf.UploadedByUser)
                .WithMany(u => u.Files)
                .HasForeignKey(pf => pf.UploadedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // 🔹 Comment -> Post
            builder.Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            // 🔹 Comment -> User
            builder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // 🔹 Self-reference Comment (reply)
            builder.Entity<Comment>()
                .HasOne(c => c.ParentComment)
                .WithMany()
                .HasForeignKey(c => c.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            // 🔹 AuditLog -> User
            builder.Entity<AuditLog>()
                .HasOne(a => a.User)
                .WithMany(u => u.AuditLogs)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
