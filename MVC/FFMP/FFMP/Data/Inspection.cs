using System;
using System.Collections.Generic;

namespace FFMP.Data
{
    public partial class Inspection
    {
        public DateTime Timestamp { get; set; }
        public string UserLogin { get; set; } = null!;
        public uint ObjectId { get; set; }
        public string Reason { get; set; } = null!;
        public string? Observations { get; set; }
        public bool? ChangeOfState { get; set; }

        public virtual User UserLoginNavigation { get; set; } = null!;
        public virtual ObjectToCheck ObjectIdNavigation { get; set; } = null!;
    }
}
