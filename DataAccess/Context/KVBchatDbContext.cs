using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Context
{
    public class KVBchatDbContext
        :DbContext
    {
        public KVBchatDbContext()
            :base("KVBchatDbContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {



            modelBuilder.Configurations.Add(new UserEntityTypeConfiguration());

            modelBuilder.Entity<User>()
                .HasMany(m => m.FirstFriend)
                .WithRequired(r => r.FirstUser)
                .HasForeignKey(f => f.IdFirst)
                .WillCascadeOnDelete(false);;

            modelBuilder.Entity<User>()
                .HasMany(m => m.SecondFriend)
                .WithRequired(r => r.SecondUser)
                .HasForeignKey(f => f.IdSecond);

            modelBuilder.Entity<User>()
                .HasMany(m => m.UserGroups)
                .WithRequired(r => r.User)
                .HasForeignKey(f => f.IdUser);

            modelBuilder.Entity<User>()
                .HasMany(m => m.GroupAdmins)
                .WithOptional(r => r.Admin)
                .HasForeignKey(f => f.IdAdmin)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(m => m.Messages)
                .WithRequired(r => r.User)
                .HasForeignKey(f => f.IdSender);

            modelBuilder.Entity<Group>()
                .HasMany(m => m.UsersGroups)
                .WithRequired(r => r.Group)
                .HasForeignKey(f => f.IdGroup);

            modelBuilder.Entity<Group>()
                .HasMany(m => m.Messages)
                .WithRequired(r => r.Group)
                .HasForeignKey(f => f.IdGroup);

            modelBuilder.Entity<Message>()
                .HasMany(m => m.Files)
                .WithRequired(r => r.Message)
                .HasForeignKey(f => f.IdMessage);

            modelBuilder.Entity<File>()
                .HasMany(m => m.MessageFiles)
                .WithRequired(r => r.File)
                .HasForeignKey(f => f.IdFile);

            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<Friend> Friends { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<MessageFile> MessageFiles { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<UsersGroup> UsersGroups { get; set; }
        public virtual DbSet<User> Users{ get; set; }
    }
}
