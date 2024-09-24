using CodingTest_BTSid.Models;
using Microsoft.EntityFrameworkCore;

namespace CodingTest_BTSid
{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options) { }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<ChecklistModel> Checklists { get; set; }
        public DbSet<ChecklistItemModel> ChecklistItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserModel>()
             .Property(u => u.UserId)
             .ValueGeneratedOnAdd();

            modelBuilder.Entity<ChecklistModel>()
                .HasMany(c => c.ToDoItems)
                .WithOne(i => i.Checklist)
                .HasForeignKey(i => i.ChecklistId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
