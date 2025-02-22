using System;
using System.Collections.Generic;
using Marafon.Models;
using Microsoft.EntityFrameworkCore;

namespace Marafon.Context;

public partial class User9Context : DbContext
{
    public User9Context()
    {
    }

    public User9Context(DbContextOptions<User9Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Charity> Charities { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Eventtype> Eventtypes { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<Marathon> Marathons { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<Racekitoption> Racekitoptions { get; set; }

    public virtual DbSet<Registration> Registrations { get; set; }

    public virtual DbSet<Registrationevent> Registrationevents { get; set; }

    public virtual DbSet<Registrationstatus> Registrationstatuses { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Runner> Runners { get; set; }

    public virtual DbSet<Sponsorship> Sponsorships { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<Timesheet> Timesheets { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Volunteer> Volunteers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=45.67.56.214;Port=5454;Database=user9;Username=user9;Password=X8C8NTnD");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Charity>(entity =>
        {
            entity.HasKey(e => e.Charityid).HasName("charityid");

            entity.ToTable("charity", "WorldSkills");

            entity.Property(e => e.Charityid).HasColumnName("charityid");
            entity.Property(e => e.Charitydescription)
                .HasMaxLength(2000)
                .HasColumnName("charitydescription");
            entity.Property(e => e.Charitylogo)
                .HasMaxLength(50)
                .HasColumnName("charitylogo");
            entity.Property(e => e.Charityname)
                .HasMaxLength(100)
                .HasColumnName("charityname");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Countrycode).HasName("countrycode");

            entity.ToTable("country", "WorldSkills");

            entity.Property(e => e.Countrycode)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasColumnName("countrycode");
            entity.Property(e => e.Countryflag)
                .HasMaxLength(100)
                .HasColumnName("countryflag");
            entity.Property(e => e.Countryname)
                .HasMaxLength(100)
                .HasColumnName("countryname");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Eventid).HasName("eventid");

            entity.ToTable("event", "WorldSkills");

            entity.Property(e => e.Eventid)
                .HasMaxLength(6)
                .IsFixedLength()
                .HasColumnName("eventid");
            entity.Property(e => e.Cost)
                .HasPrecision(10, 2)
                .HasColumnName("cost");
            entity.Property(e => e.Eventname)
                .HasMaxLength(50)
                .HasColumnName("eventname");
            entity.Property(e => e.Eventtypeid)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("eventtypeid");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Marathonid).HasColumnName("marathonid");
            entity.Property(e => e.Maxparticipants).HasColumnName("maxparticipants");
            entity.Property(e => e.Startdatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("startdatetime");

            entity.HasOne(d => d.Eventtype).WithMany(p => p.Events)
                .HasForeignKey(d => d.Eventtypeid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("event_eventtypeid_fkey");

            entity.HasOne(d => d.Marathon).WithMany(p => p.Events)
                .HasForeignKey(d => d.Marathonid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("event_marathonid_fkey");
        });

        modelBuilder.Entity<Eventtype>(entity =>
        {
            entity.HasKey(e => e.Eventtypeid).HasName("eventtypeid");

            entity.ToTable("eventtype", "WorldSkills");

            entity.Property(e => e.Eventtypeid)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("eventtypeid");
            entity.Property(e => e.Eventtypename)
                .HasMaxLength(50)
                .HasColumnName("eventtypename");
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.HasKey(e => e.Gender1).HasName("gender");

            entity.ToTable("Gender", "WorldSkills");

            entity.Property(e => e.Gender1)
                .HasMaxLength(10)
                .HasColumnName("gender");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
        });

        modelBuilder.Entity<Marathon>(entity =>
        {
            entity.HasKey(e => e.Marathonid).HasName("marathonid");

            entity.ToTable("marathon", "WorldSkills");

            entity.Property(e => e.Marathonid).HasColumnName("marathonid");
            entity.Property(e => e.Cityname)
                .HasMaxLength(80)
                .HasColumnName("cityname");
            entity.Property(e => e.Countrycode)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasColumnName("countrycode");
            entity.Property(e => e.Marathonname)
                .HasMaxLength(80)
                .HasColumnName("marathonname");
            entity.Property(e => e.Yearheld).HasColumnName("yearheld");

            entity.HasOne(d => d.CountrycodeNavigation).WithMany(p => p.Marathons)
                .HasForeignKey(d => d.Countrycode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("marathon_countrycode_fkey");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.Positionid).HasName("PositionId");

            entity.ToTable("position", "WorldSkills");

            entity.Property(e => e.Positionid).HasColumnName("positionid");
            entity.Property(e => e.Payperiod)
                .HasColumnType("character varying")
                .HasColumnName("payperiod");
            entity.Property(e => e.Payrate).HasColumnName("payrate");
            entity.Property(e => e.Positiondescription)
                .HasColumnType("character varying")
                .HasColumnName("positiondescription");
            entity.Property(e => e.Positionname)
                .HasColumnType("character varying")
                .HasColumnName("positionname");
        });

        modelBuilder.Entity<Racekitoption>(entity =>
        {
            entity.HasKey(e => e.Racekitoptionid).HasName("racekitoptionid");

            entity.ToTable("racekitoption", "WorldSkills");

            entity.Property(e => e.Racekitoptionid)
                .HasMaxLength(1)
                .ValueGeneratedNever()
                .HasColumnName("racekitoptionid");
            entity.Property(e => e.Cost)
                .HasPrecision(10, 2)
                .HasColumnName("cost");
            entity.Property(e => e.Racekitoption1)
                .HasMaxLength(80)
                .HasColumnName("racekitoption");
        });

        modelBuilder.Entity<Registration>(entity =>
        {
            entity.HasKey(e => e.Registrationid).HasName("registrationid");

            entity.ToTable("registration", "WorldSkills");

            entity.Property(e => e.Registrationid).HasColumnName("registrationid");
            entity.Property(e => e.Charityid).HasColumnName("charityid");
            entity.Property(e => e.Cost)
                .HasPrecision(10, 2)
                .HasColumnName("cost");
            entity.Property(e => e.Racekitoptionid)
                .HasMaxLength(1)
                .HasColumnName("racekitoptionid");
            entity.Property(e => e.Registrationdatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("registrationdatetime");
            entity.Property(e => e.Registrationstatusid).HasColumnName("registrationstatusid");
            entity.Property(e => e.Runnerid).HasColumnName("runnerid");
            entity.Property(e => e.Sponsorshiptarget)
                .HasPrecision(10, 2)
                .HasColumnName("sponsorshiptarget");

            entity.HasOne(d => d.Charity).WithMany(p => p.Registrations)
                .HasForeignKey(d => d.Charityid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("registration_charityid_fkey");

            entity.HasOne(d => d.Racekitoption).WithMany(p => p.Registrations)
                .HasForeignKey(d => d.Racekitoptionid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("registration_racekitoptionid_fkey");

            entity.HasOne(d => d.Registrationstatus).WithMany(p => p.Registrations)
                .HasForeignKey(d => d.Registrationstatusid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("registration_registrationstatusid_fkey");

            entity.HasOne(d => d.Runner).WithMany(p => p.Registrations)
                .HasForeignKey(d => d.Runnerid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("registration_runnerid_fkey");
        });

        modelBuilder.Entity<Registrationevent>(entity =>
        {
            entity.HasKey(e => e.Registrationeventid).HasName("registrationeventid");

            entity.ToTable("registrationevent", "WorldSkills");

            entity.Property(e => e.Registrationeventid).HasColumnName("registrationeventid");
            entity.Property(e => e.Bibnumber).HasColumnName("bibnumber");
            entity.Property(e => e.Eventid)
                .HasMaxLength(6)
                .IsFixedLength()
                .HasColumnName("eventid");
            entity.Property(e => e.Racetime).HasColumnName("racetime");
            entity.Property(e => e.Registrationid).HasColumnName("registrationid");

            entity.HasOne(d => d.Event).WithMany(p => p.Registrationevents)
                .HasForeignKey(d => d.Eventid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("registrationevent_eventid_fkey");

            entity.HasOne(d => d.Registration).WithMany(p => p.Registrationevents)
                .HasForeignKey(d => d.Registrationid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("registrationevent_registrationid_fkey");
        });

        modelBuilder.Entity<Registrationstatus>(entity =>
        {
            entity.HasKey(e => e.Registrationstatusid).HasName("registrationstatusid");

            entity.ToTable("registrationstatus", "WorldSkills");

            entity.Property(e => e.Registrationstatusid).HasColumnName("registrationstatusid");
            entity.Property(e => e.Registrationstatus1)
                .HasMaxLength(80)
                .HasColumnName("registrationstatus");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Roleid).HasName("roleid");

            entity.ToTable("role", "WorldSkills");

            entity.Property(e => e.Roleid)
                .HasMaxLength(1)
                .ValueGeneratedNever()
                .HasColumnName("roleid");
            entity.Property(e => e.Rolename)
                .HasMaxLength(50)
                .HasColumnName("rolename");
        });

        modelBuilder.Entity<Runner>(entity =>
        {
            entity.HasKey(e => e.Runnerid).HasName("runnerid");

            entity.ToTable("runner", "WorldSkills");

            entity.Property(e => e.Runnerid).HasColumnName("runnerid");
            entity.Property(e => e.Countrycode)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasColumnName("countrycode");
            entity.Property(e => e.Dateofbirth)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("dateofbirth");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .HasColumnName("gender");

            entity.HasOne(d => d.CountrycodeNavigation).WithMany(p => p.Runners)
                .HasForeignKey(d => d.Countrycode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("runner_countrycode_fkey");

            entity.HasOne(d => d.EmailNavigation).WithMany(p => p.Runners)
                .HasForeignKey(d => d.Email)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("runner_email_fkey");

            entity.HasOne(d => d.GenderNavigation).WithMany(p => p.Runners)
                .HasForeignKey(d => d.Gender)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("runner_gender_fkey");
        });

        modelBuilder.Entity<Sponsorship>(entity =>
        {
            entity.HasKey(e => e.Sponsorshipid).HasName("sponsorshipid");

            entity.ToTable("sponsorship", "WorldSkills");

            entity.Property(e => e.Sponsorshipid).HasColumnName("sponsorshipid");
            entity.Property(e => e.Amount)
                .HasPrecision(10, 2)
                .HasColumnName("amount");
            entity.Property(e => e.Registrationid).HasColumnName("registrationid");
            entity.Property(e => e.Sponsorname)
                .HasMaxLength(150)
                .HasColumnName("sponsorname");

            entity.HasOne(d => d.Registration).WithMany(p => p.Sponsorships)
                .HasForeignKey(d => d.Registrationid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sponsorship_registrationid_fkey");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.Staffid).HasName("StaffId");

            entity.ToTable("staff", "WorldSkills");

            entity.Property(e => e.Staffid).HasColumnName("staffid");
            entity.Property(e => e.Comments)
                .HasColumnType("character varying")
                .HasColumnName("comments");
            entity.Property(e => e.Dateofbirth)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("dateofbirth");
            entity.Property(e => e.Email)
                .HasColumnType("character varying")
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasColumnType("character varying")
                .HasColumnName("firstname");
            entity.Property(e => e.Gender)
                .HasColumnType("character varying")
                .HasColumnName("gender");
            entity.Property(e => e.Lastname)
                .HasColumnType("character varying")
                .HasColumnName("lastname");
            entity.Property(e => e.Postionid).HasColumnName("postionid");

            entity.HasOne(d => d.GenderNavigation).WithMany(p => p.Staff)
                .HasForeignKey(d => d.Gender)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("staff_gender_fk");

            entity.HasOne(d => d.Postion).WithMany(p => p.Staff)
                .HasForeignKey(d => d.Postionid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("staff_position_fk");
        });

        modelBuilder.Entity<Timesheet>(entity =>
        {
            entity.HasKey(e => e.Timesheetid).HasName("TimesheetId");

            entity.ToTable("timesheet", "WorldSkills");

            entity.Property(e => e.Timesheetid).HasColumnName("timesheetid");
            entity.Property(e => e.Enddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("enddatetime");
            entity.Property(e => e.Payamount).HasColumnName("payamount");
            entity.Property(e => e.Staffid).HasColumnName("staffid");
            entity.Property(e => e.Startdatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("startdatetime");

            entity.HasOne(d => d.Staff).WithMany(p => p.Timesheets)
                .HasForeignKey(d => d.Staffid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("timesheet_staff_fk");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("email");

            entity.ToTable("User", "WorldSkills");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(80)
                .HasColumnName("firstname");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Lastname)
                .HasMaxLength(80)
                .HasColumnName("lastname");
            entity.Property(e => e.Roleid)
                .HasMaxLength(1)
                .HasColumnName("roleid");
            entity.Property(e => e.Userpassword)
                .HasMaxLength(100)
                .HasColumnName("userpassword");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.Roleid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("User_roleid_fkey");
        });

        modelBuilder.Entity<Volunteer>(entity =>
        {
            entity.HasKey(e => e.Volunteerid).HasName("volunteerid");

            entity.ToTable("volunteer", "WorldSkills");

            entity.Property(e => e.Volunteerid).HasColumnName("volunteerid");
            entity.Property(e => e.Countrycode)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasColumnName("countrycode");
            entity.Property(e => e.Firstname)
                .HasMaxLength(80)
                .HasColumnName("firstname");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .HasColumnName("gender");
            entity.Property(e => e.Lastname)
                .HasMaxLength(80)
                .HasColumnName("lastname");

            entity.HasOne(d => d.CountrycodeNavigation).WithMany(p => p.Volunteers)
                .HasForeignKey(d => d.Countrycode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("volunteer_countrycode_fkey");

            entity.HasOne(d => d.GenderNavigation).WithMany(p => p.Volunteers)
                .HasForeignKey(d => d.Gender)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("volunteer_gender_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
