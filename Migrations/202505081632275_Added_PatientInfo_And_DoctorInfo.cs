namespace MVCDocterPatientSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_PatientInfo_And_DoctorInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PatientInfoes", "Image", c => c.Binary());
            AddColumn("dbo.Appointments", "AppointmentDateTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Appointments", "appointmentDate");
            DropColumn("dbo.Appointments", "TimeSpan");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Appointments", "TimeSpan", c => c.DateTime(nullable: false));
            AddColumn("dbo.Appointments", "appointmentDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Appointments", "AppointmentDateTime");
            DropColumn("dbo.PatientInfoes", "Image");
        }
    }
}
