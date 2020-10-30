using System;
using DataAccessLayer.Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataAccessLayer.Context
{
    public partial class TodoListDBContext : DbContext
    {
        private static TodoListDBContext instance;
        private TodoListDBContext()
        {
        }
        public static TodoListDBContext getInstance()
        {
            if (instance == null)
            {
                instance = new TodoListDBContext();
            }

            return instance;
        }

        public TodoListDBContext(DbContextOptions<TodoListDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Work> Work { get; set; }
        public virtual DbSet<WorkEmployee> WorkEmployee { get; set; }
        public virtual DbSet<WorkList> WorkList { get; set; }
        public virtual DbSet<WorkListEmployee> WorkListEmployee { get; set; }
        public virtual DbSet<WorkListStatus> WorkListStatus { get; set; }
        public virtual DbSet<WorkStatus> WorkStatus { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=TodoListDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(e => e.CommentContent).HasMaxLength(200);

                entity.Property(e => e.DateCreated)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.IdEmployee)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Comment_Employee");

                entity.HasOne(d => d.IdWorkNavigation)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.IdWork)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Comment_Work");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.DateCreated)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FullName).HasMaxLength(100);

                entity.Property(e => e.IdRole).HasDefaultValueSql("((1))");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.IdRole)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("User_Role");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.DateCreated)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Role1)
                    .HasColumnName("Role")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Work>(entity =>
            {
                entity.Property(e => e.DateCreated)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdWorkStatus).HasDefaultValueSql("((1))");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.WorkContent).HasMaxLength(200);

                entity.HasOne(d => d.IdWorkListNavigation)
                    .WithMany(p => p.Work)
                    .HasForeignKey(d => d.IdWorkList)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Work_WorkList");

                entity.HasOne(d => d.IdWorkStatusNavigation)
                    .WithMany(p => p.Work)
                    .HasForeignKey(d => d.IdWorkStatus)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Work_WorkStatus");
            });

            modelBuilder.Entity<WorkEmployee>(entity =>
            {
                entity.ToTable("Work_Employee");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.WorkEmployee)
                    .HasForeignKey(d => d.IdEmployee)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("WorkEmployee_Employee");

                entity.HasOne(d => d.IdWorkNavigation)
                    .WithMany(p => p.WorkEmployee)
                    .HasForeignKey(d => d.IdWork)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("WorkEmployee_Work");
            });

            modelBuilder.Entity<WorkList>(entity =>
            {
                entity.Property(e => e.DateCreated)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdWorkListStatus).HasDefaultValueSql("((1))");

                entity.Property(e => e.WorkListName).HasMaxLength(100);

                entity.HasOne(d => d.IdWorkListStatusNavigation)
                    .WithMany(p => p.WorkList)
                    .HasForeignKey(d => d.IdWorkListStatus)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("WorkList_WorkListStatus");
            });

            modelBuilder.Entity<WorkListEmployee>(entity =>
            {
                entity.ToTable("WorkList_Employee");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.WorkListEmployee)
                    .HasForeignKey(d => d.IdEmployee)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("WorkListEmployee_Employee");

                entity.HasOne(d => d.IdWorkListNavigation)
                    .WithMany(p => p.WorkListEmployee)
                    .HasForeignKey(d => d.IdWorkList)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("WorkListEmployee_WorkList");
            });

            modelBuilder.Entity<WorkListStatus>(entity =>
            {
                entity.Property(e => e.DateCreated)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.WorkListStatus1)
                    .HasColumnName("WorkListStatus")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<WorkStatus>(entity =>
            {
                entity.Property(e => e.DateCreated)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.WorkStatus1)
                    .HasColumnName("WorkStatus")
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}