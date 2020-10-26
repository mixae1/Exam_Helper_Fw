﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Exam_Helper
{
    public partial class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbContext()
        {
        }

        public DbContext(DbContextOptions<DbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ATest> ATest { get; set; }
        public virtual DbSet<Complaint> Complaint { get; set; }
        public virtual DbSet<Pack> Pack { get; set; }
        public virtual DbSet<Question> Question { get; set; }
        public virtual DbSet<Tags> Tags { get; set; }
        public virtual DbSet<Tests> Tests { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=ec2-52-207-124-89.compute-1.amazonaws.com;Port=5432;Database=ds38mc84b76g4;Username=cfxdaexdfxisyl;Password=9e9e4125262f940ee3e007f56412917c23e01f2a15b40d325f1a5206ccc8de0b;SslMode=Require;Trustservercertificate=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ATest>(entity =>
            {
                entity.ToTable("aTest");

                entity.Property(e => e.Id).HasDefaultValueSql("nextval('atest_id_seq'::regclass)");

                entity.Property(e => e.ObjectId).HasColumnName("object_id");

                entity.Property(e => e.Serviceinfo)
                    .IsRequired()
                    .HasColumnName("serviceinfo")
                    .HasColumnType("character varying");

                entity.Property(e => e.TypeId).HasColumnName("type_id");

                entity.HasOne(d => d.Object)
                    .WithMany(p => p.ATest)
                    .HasForeignKey(d => d.ObjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("atest_pack_fkey");

                entity.HasOne(d => d.ObjectNavigation)
                    .WithMany(p => p.ATest)
                    .HasForeignKey(d => d.ObjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("atest_ques_fkey");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.ATest)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("atest_test_fkey");
            });

            modelBuilder.Entity<Complaint>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Info)
                    .IsRequired()
                    .HasColumnName("info")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Pack>(entity =>
            {
                entity.HasComment("Pack");

                entity.Property(e => e.Id).HasDefaultValueSql("nextval('pack_id_seq'::regclass)");

                entity.Property(e => e.Author)
                    .IsRequired()
                    .HasColumnName("author")
                    .HasColumnType("character varying");

                entity.Property(e => e.CreationDate)
                    .HasColumnName("creation_date")
                    .HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("character varying");

                entity.Property(e => e.QuestionSet)
                    .IsRequired()
                    .HasColumnName("question_set")
                    .HasColumnType("character varying");

                entity.Property(e => e.TagsId)
                    .IsRequired()
                    .HasColumnName("tags_id")
                    .HasColumnType("character varying");

                entity.Property(e => e.UpdateDate)
                    .HasColumnName("update_date")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.Property(e => e.Author)
                    .IsRequired()
                    .HasColumnName("author")
                    .HasColumnType("character varying");

                entity.Property(e => e.CreationDate)
                    .HasColumnName("creation_date")
                    .HasColumnType("date");

                entity.Property(e => e.Definition)
                    .IsRequired()
                    .HasColumnName("definition")
                    .HasColumnType("character varying");

                entity.Property(e => e.Proof)
                    .IsRequired()
                    .HasColumnName("proof")
                    .HasColumnType("character varying");

                entity.Property(e => e.TagIds)
                    .IsRequired()
                    .HasColumnName("tag_ids")
                    .HasColumnType("character varying");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasColumnType("character varying");

                entity.Property(e => e.UpdateDate)
                    .HasColumnName("update_date")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<Tags>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("nextval('tags_id_seq'::regclass)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<Tests>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("nextval('tests_id_seq'::regclass)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("character varying");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnName("type")
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasComment("User");

                entity.Property(e => e.Id).HasDefaultValueSql("nextval('user_id_seq'::regclass)");

                entity.Property(e => e.IgnorePacks)
                    .HasColumnName("ignore_packs")
                    .HasColumnType("character varying");

                entity.Property(e => e.IgnoreQues)
                    .HasColumnName("ignore_ques")
                    .HasColumnType("character varying");

                entity.Property(e => e.Img)
                    .HasColumnName("img")
                    .HasColumnType("character varying");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnName("login")
                    .HasColumnType("character varying");

                entity.Property(e => e.PackSet)
                    .HasColumnName("pack_set")
                    .HasColumnType("character varying");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasColumnType("character varying");

                entity.Property(e => e.QuestionSet)
                    .HasColumnName("question_set")
                    .HasColumnType("character varying");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasColumnType("character varying");
            });

            modelBuilder.HasSequence("atest_id_seq");

            modelBuilder.HasSequence("pack_id_seq");

            modelBuilder.HasSequence("tags_id_seq");

            modelBuilder.HasSequence("tests_id_seq");

            modelBuilder.HasSequence("user_id_seq");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
