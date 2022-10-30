using Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class Comment : AuditableBaseEntity
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public Post Post { get; set; }
        public User User { get; set; }
    }
}
