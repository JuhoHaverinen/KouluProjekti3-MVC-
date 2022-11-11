using System;
using System.Collections.Generic;

namespace FFMP.Data
{
    public partial class AuditingForm
    {
        public AuditingForm()
        {
            Requirements = new HashSet<Requirement>();
        }

        public uint AuditingId { get; set; }
        public string Maker { get; set; } = null!;
        public uint TargetGroupId { get; set; }
        public DateTime Created { get; set; }
        public string Description { get; set; } = null!;

        public virtual User MakerNavigation { get; set; } = null!;
        public virtual TargetGroup TargetGroup { get; set; } = null!;
        public virtual ICollection<Requirement> Requirements { get; set; }
    }
}
