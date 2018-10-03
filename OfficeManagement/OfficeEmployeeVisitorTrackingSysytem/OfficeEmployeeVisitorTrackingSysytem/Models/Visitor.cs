namespace OfficeEmployeeVisitorTrackingSysytem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Visitor")]
    public partial class Visitor
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? CompanyId { get; set; }

        public int? EmployeeId { get; set; }

        public string CarNumber { get; set; }

        public string Email { get; set; }

        public DateTime? LogInTime { get; set; }

        public DateTime? LogOutTime { get; set; }

        public string Status { get; set; }

        public virtual Company Company { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
