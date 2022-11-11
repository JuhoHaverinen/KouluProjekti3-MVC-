using System;
using System.Collections.Generic;

namespace FFMP.Data
{
    public partial class Object
    {
        public Object()
        {
            AuditingLogs = new HashSet<AuditingLog>();
            Inspections = new HashSet<Inspection>();
        }

        public string Id { get; set; } = null!;
        public string Creator { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string TargetGroup { get; set; } = null!;
        public string Location { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string Model { get; set; } = null!;
        public bool? State { get; set; }
        public DateTime Created { get; set; }
        public uint TargetGroupId { get; set; }

        public virtual User CreatorNavigation { get; set; } = null!;
        public virtual TargetGroup TargetGroupNavigation { get; set; } = null!;
        public virtual ICollection<AuditingLog> AuditingLogs { get; set; }
        public virtual ICollection<Inspection> Inspections { get; set; }
    }
}
