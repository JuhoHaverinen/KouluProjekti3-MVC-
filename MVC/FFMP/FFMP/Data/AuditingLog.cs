using System;
using System.Collections.Generic;

namespace FFMP.Data
{
    public partial class AuditingLog
    {
        public AuditingLog()
        {
            RequirementResults = new HashSet<RequirementResult>();
        }

        public uint Id { get; set; }
        public string Maker { get; set; } = null!;
        public string Object { get; set; } = null!;
        public DateTime? Created { get; set; }
        public string? Description { get; set; }
        public string? Result { get; set; }

        public virtual User MakerNavigation { get; set; } = null!;
        public virtual Object ObjectNavigation { get; set; } = null!;
        public virtual ICollection<RequirementResult> RequirementResults { get; set; }
    }
}
