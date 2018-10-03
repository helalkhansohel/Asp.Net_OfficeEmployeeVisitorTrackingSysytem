namespace OfficeEmployeeVisitorTrackingSysytem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HotDesk")]
    public partial class HotDesk
    {
        public int Id { get; set; }

        public int? EmployeeId { get; set; }

        public int? CompanyId { get; set; }

        public string CarNumber { get; set; }

        public string Email { get; set; }

        public DateTime? Date { get; set; }

        public DateTime? LogInTime { get; set; }

        public DateTime? LogOutTime { get; set; }

        public string CurrentStatus { get; set; }

        public virtual Company Company { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
