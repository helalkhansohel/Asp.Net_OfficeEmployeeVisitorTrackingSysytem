namespace OfficeEmployeeVisitorTrackingSysytem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Office")]
    public partial class Office
    {
        public int Id { get; set; }

        public int? EmployeeId { get; set; }

        public DateTime? Date { get; set; }

        public DateTime? LogInTime { get; set; }

        public DateTime? LogOutTime { get; set; }

        public string CurrentStatus { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
