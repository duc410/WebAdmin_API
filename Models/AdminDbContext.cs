using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project3_WebAPIadmin.Models
{
    public class AdminDbContext : DbContext
    {
        public AdminDbContext()
        {
        }

        public AdminDbContext(DbContextOptions<AdminDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Tenants> Tenants { get; set; }
        public virtual DbSet<Rights> Rights { get; set; }
        public virtual DbSet<User2Roles> User2Roles { get; set; }
        public virtual DbSet<Role2Rights> Role2Rights { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
              .HasAnnotation("Relational:MaxIdentifierLength", 128)
              .HasAnnotation("ProductVersion", "5.0.10")
              .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Project3_WebAPIadmin.Models.Rights", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<string>("Description")
                    .HasColumnType("nvarchar(max)");

                b.Property<bool>("IsEnabled")
                    .HasColumnType("bit");

                b.Property<string>("Name")
                    .HasColumnType("nvarchar(max)");

                b.HasKey("Id");

                b.ToTable("Rights");
            });

            modelBuilder.Entity("Project3_WebAPIadmin.Models.Role2Rights", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<string>("Name")
                    .HasColumnType("nvarchar(max)");

                b.Property<int>("RightId")
                    .HasColumnType("int");

                b.Property<int>("RoleId")
                    .HasColumnType("int");

                b.HasKey("Id");

                b.HasIndex("RightId");

                b.HasIndex("RoleId");

                b.ToTable("Role2Rights");
            });

            modelBuilder.Entity("Project3_WebAPIadmin.Models.Roles", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<string>("Description")
                    .HasColumnType("nvarchar(max)");

                b.Property<bool>("IsEnabled")
                    .HasColumnType("bit");

                b.Property<string>("Name")
                    .HasColumnType("nvarchar(max)");

                b.Property<int>("TenantId")
                    .HasColumnType("int");

                b.HasKey("Id");

                b.HasIndex("TenantId");

                b.ToTable("Roles");
            });

            modelBuilder.Entity("Project3_WebAPIadmin.Models.Tenants", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<string>("Name")
                    .HasColumnType("nvarchar(max)");

                b.HasKey("Id");

                b.ToTable("Tenants");
            });

            modelBuilder.Entity("Project3_WebAPIadmin.Models.User2Roles", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<string>("Name")
                    .HasColumnType("nvarchar(max)");

                b.Property<int>("RoleId")
                    .HasColumnType("int");

                b.Property<int>("UserId")
                    .HasColumnType("int");

                b.HasKey("Id");

                b.HasIndex("RoleId");

                b.HasIndex("UserId");

                b.ToTable("User2Roles");
            });

            modelBuilder.Entity("Project3_WebAPIadmin.Models.Users", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<string>("Code")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("FullName")
                    .HasColumnType("nvarchar(max)");

                b.Property<bool>("IsEnabled")
                    .HasColumnType("bit");

                b.Property<string>("Note")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Password")
                    .HasColumnType("nvarchar(max)");

                b.Property<int>("TenantId")
                    .HasColumnType("int");

                b.HasKey("Id");

                b.HasIndex("TenantId");

                b.ToTable("Users");
            });

            modelBuilder.Entity("Project3_WebAPIadmin.Models.Role2Rights", b =>
            {
                b.HasOne("Project3_WebAPIadmin.Models.Rights", "Right")
                    .WithMany("Role2Rights")
                    .HasForeignKey("RightId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .IsRequired();

                b.HasOne("Project3_WebAPIadmin.Models.Roles", "Role")
                    .WithMany("Role2Rights")
                    .HasForeignKey("RoleId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .IsRequired();

                b.Navigation("Right");

                b.Navigation("Role");
            });

            modelBuilder.Entity("Project3_WebAPIadmin.Models.Roles", b =>
            {
                b.HasOne("Project3_WebAPIadmin.Models.Tenants", "Tenant")
                    .WithMany("Roles")
                    .HasForeignKey("TenantId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Tenant");
            });

            modelBuilder.Entity("Project3_WebAPIadmin.Models.User2Roles", b =>
            {
                b.HasOne("Project3_WebAPIadmin.Models.Roles", "Role")
                    .WithMany("User2Roles")
                    .HasForeignKey("RoleId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .IsRequired();

                b.HasOne("Project3_WebAPIadmin.Models.Users", "User")
                    .WithMany("User2Roles")
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .IsRequired();

                b.Navigation("Role");

                b.Navigation("User");
            });

            modelBuilder.Entity("Project3_WebAPIadmin.Models.Users", b =>
            {
                b.HasOne("Project3_WebAPIadmin.Models.Tenants", "Tenant")
                    .WithMany("Users")
                    .HasForeignKey("TenantId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Tenant");
            });

            modelBuilder.Entity("Project3_WebAPIadmin.Models.Rights", b =>
            {
                b.Navigation("Role2Rights");
            });

            modelBuilder.Entity("Project3_WebAPIadmin.Models.Roles", b =>
            {
                b.Navigation("Role2Rights");

                b.Navigation("User2Roles");
            });

            modelBuilder.Entity("Project3_WebAPIadmin.Models.Tenants", b =>
            {
                b.Navigation("Roles");

                b.Navigation("Users");
            });

            modelBuilder.Entity("Project3_WebAPIadmin.Models.Users", b =>
            {
                b.Navigation("User2Roles");
            });
        }
    }
}
