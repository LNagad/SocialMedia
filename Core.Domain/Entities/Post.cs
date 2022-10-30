using Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class Post : AuditableBaseEntity
    {
        public string? Tittle { get; set; }
        public string? Body { get; set; }
        public string? ImagePost { get; set; }
        public int UserId { get; set; }  
        public User User { get; set; }
        public ICollection<Comment>? Comments { get; set; }
    }
}
