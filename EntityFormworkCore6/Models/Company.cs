using System;
using System.Collections.Generic;

namespace EntityFormworkCore6.Models
{
    public partial class Company
    {
        public Company()
        {
            SysUsers = new HashSet<SysUser>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? CreateTime { get; set; }
        public int CreatorId { get; set; }
        public int? LastModifierId { get; set; }
        public DateTime? LastModifyTime { get; set; }

        public virtual ICollection<SysUser> SysUsers { get; set; }
    }
}
