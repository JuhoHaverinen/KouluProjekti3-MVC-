using System;
using System.Collections.Generic;

namespace FFMP.Data
{
    public partial class Inspection
    {
        public DateTime Timestamp { get; set; }
        public string Maker { get; set; } = null!;
        public string ObjectId { get; set; } = null!;
        public string Reason { get; set; } = null!;
        public string? Observations { get; set; }
        public bool? ChangeOfState { get; set; }

        public virtual User MakerNavigation { get; set; } = null!;
        public virtual Object Object { get; set; } = null!;
    }
}
