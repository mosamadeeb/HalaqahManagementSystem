using System;
using System.Collections.Generic;
using HalaqahModel.Models;
using Microsoft.EntityFrameworkCore;

namespace HalaqahModel.Context;

public partial class HalaqahContext : DbContext
{
    public HalaqahContext()
    {
        
    }

    public HalaqahContext(DbContextOptions<HalaqahContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Halaqah> Halaqahs { get; set; }

    public virtual DbSet<HalaqahRecord> HalaqahRecords { get; set; }

    public virtual DbSet<Masjid> Masjids { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Record> Records { get; set; }

    public virtual DbSet<Segment> Segments { get; set; }

    public virtual DbSet<Semester> Semesters { get; set; }

    public virtual DbSet<SemesterRecord> SemesterRecords { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentAttendance> StudentAttendances { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAttendance> UserAttendances { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=HMSv2;Trusted_Connection=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

        modelBuilder.Entity<Halaqah>(entity =>
        {
            entity.ToTable("Halaqah");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AdminId).HasColumnName("AdminID");
            entity.Property(e => e.Name).HasMaxLength(80);
            entity.Property(e => e.SemesterId).HasColumnName("SemesterID");
            entity.Property(e => e.TeacherId).HasColumnName("TeacherID");

            entity.HasOne(d => d.Admin).WithMany(p => p.HalaqahAdmins)
                .HasForeignKey(d => d.AdminId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Halaqah_UserAdmin");

            entity.HasOne(d => d.Semester).WithMany(p => p.Halaqahs)
                .HasForeignKey(d => d.SemesterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Halaqah_Semester");

            entity.HasOne(d => d.Teacher).WithMany(p => p.HalaqahTeachers)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Halaqah_UserTeacher");

            entity.HasMany(d => d.Students).WithMany(p => p.Halaqahs)
                .UsingEntity<Dictionary<string, object>>(
                    "HalaqahStudent",
                    r => r.HasOne<Student>().WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_HalaqahStudent_Student"),
                    l => l.HasOne<Halaqah>().WithMany()
                        .HasForeignKey("HalaqahId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_HalaqahStudent_Halaqah"),
                    j =>
                    {
                        j.HasKey("HalaqahId", "StudentId");
                        j.ToTable("HalaqahStudent");
                        j.IndexerProperty<int>("HalaqahId").HasColumnName("HalaqahID");
                        j.IndexerProperty<int>("StudentId").HasColumnName("StudentID");
                    });
        });

        modelBuilder.Entity<HalaqahRecord>(entity =>
        {
            entity.HasKey(e => new { e.Date, e.StudentId }).HasName("PK__tmp_ms_x__F4142FA1F4F98435");

            entity.ToTable("HalaqahRecord");

            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.HifzRecordId).HasColumnName("HifzRecordID");
            entity.Property(e => e.RevisionRecordId).HasColumnName("RevisionRecordID");

            entity.HasOne(d => d.HifzRecord).WithMany(p => p.HalaqahRecordHifzRecords)
                .HasForeignKey(d => d.HifzRecordId)
                .HasConstraintName("FK_HalaqahRecord_HifzRecord");

            entity.HasOne(d => d.RevisionRecord).WithMany(p => p.HalaqahRecordRevisionRecords)
                .HasForeignKey(d => d.RevisionRecordId)
                .HasConstraintName("FK_HalaqahRecord_RevisionRecord");

            entity.HasOne(d => d.Student).WithMany(p => p.HalaqahRecords)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HalaqahRecord_Student");
        });

        modelBuilder.Entity<Masjid>(entity =>
        {
            entity.ToTable("Masjid");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.ToTable("Person");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.MasjidId).HasColumnName("MasjidID");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Surname).HasMaxLength(50);

            entity.HasOne(d => d.Masjid).WithMany(p => p.People)
                .HasForeignKey(d => d.MasjidId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Person_Masjid");
        });

        modelBuilder.Entity<Record>(entity =>
        {
            entity.ToTable("Record");

            entity.Property(e => e.Id).HasColumnName("ID");
        });

        modelBuilder.Entity<Segment>(entity =>
        {
            entity.HasKey(e => new { e.SegmentId, e.RecordId });

            entity.ToTable("Segment");

            entity.Property(e => e.SegmentId)
                .ValueGeneratedOnAdd()
                .HasColumnName("SegmentID");
            entity.Property(e => e.RecordId).HasColumnName("RecordID");
            entity.Property(e => e.SurahFrom).HasMaxLength(50);
            entity.Property(e => e.SurahTo).HasMaxLength(50);

            entity.HasOne(d => d.Record).WithMany(p => p.Segments)
                .HasForeignKey(d => d.RecordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Segment_Record");
        });

        modelBuilder.Entity<Semester>(entity =>
        {
            entity.ToTable("Semester");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<SemesterRecord>(entity =>
        {
            entity.HasKey(e => new { e.StudentId, e.SemesterId });

            entity.ToTable("SemesterRecord");

            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.SemesterId).HasColumnName("SemesterID");

            entity.HasOne(d => d.HifzExistingRecordNavigation).WithMany(p => p.SemesterRecordHifzExistingRecordNavigations)
                .HasForeignKey(d => d.HifzExistingRecord)
                .HasConstraintName("FK_SemesterRecord_HifzExistingRecord");

            entity.HasOne(d => d.HifzTargetRecordNavigation).WithMany(p => p.SemesterRecordHifzTargetRecordNavigations)
                .HasForeignKey(d => d.HifzTargetRecord)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SemesterRecord_HifzTargetRecord");

            entity.HasOne(d => d.RevisionExistingRecordNavigation).WithMany(p => p.SemesterRecordRevisionExistingRecordNavigations)
                .HasForeignKey(d => d.RevisionExistingRecord)
                .HasConstraintName("FK_SemesterRecord_RevisionExistingRecord");

            entity.HasOne(d => d.RevisionTargetRecordNavigation).WithMany(p => p.SemesterRecordRevisionTargetRecordNavigations)
                .HasForeignKey(d => d.RevisionTargetRecord)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SemesterRecord_RevisionTargetRecord");

            entity.HasOne(d => d.Semester).WithMany(p => p.SemesterRecords)
                .HasForeignKey(d => d.SemesterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SemesterRecord_Semester");

            entity.HasOne(d => d.Student).WithMany(p => p.SemesterRecords)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SemesterRecord_Student");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.PersonId);

            entity.ToTable("Student");

            entity.Property(e => e.PersonId)
                .ValueGeneratedNever()
                .HasColumnName("PersonID");
            entity.Property(e => e.ParentPhone)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Person).WithOne(p => p.Student)
                .HasForeignKey<Student>(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Student_Person");
        });

        modelBuilder.Entity<StudentAttendance>(entity =>
        {
            entity.HasKey(e => new { e.StudentId, e.Timestamp });

            entity.ToTable("StudentAttendance");

            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.Timestamp).HasColumnType("datetime");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentAttendances)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentAttendance_Student");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.PersonId);

            entity.ToTable("User");

            entity.Property(e => e.PersonId)
                .ValueGeneratedNever()
                .HasColumnName("PersonID");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Nationality).HasMaxLength(50);
            entity.Property(e => e.PasswordHash).HasMaxLength(50);

            entity.HasOne(d => d.Person).WithOne(p => p.User)
                .HasForeignKey<User>(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Person");
        });

        modelBuilder.Entity<UserAttendance>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.Timestamp }).HasName("PK_Attendance");

            entity.ToTable("UserAttendance");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Timestamp).HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.UserAttendances)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Attendance_User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
