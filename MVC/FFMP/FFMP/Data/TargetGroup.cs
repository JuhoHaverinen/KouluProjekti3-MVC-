using System;
using System.Collections.Generic;

namespace FFMP.Data
{
    public partial class TargetGroup
    {
        public TargetGroup()
        {
            AuditingForms = new HashSet<AuditingForm>();
            Objects = new HashSet<ObjectToCheck>();
        }

        public uint Id { get; set; }
        public string Description { get; set; } = null!;

        public virtual ICollection<AuditingForm> AuditingForms { get; set; }
        public virtual ICollection<ObjectToCheck> Objects { get; set; }
    }
}
