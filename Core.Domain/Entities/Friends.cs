using Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class Friends : AuditableBaseEntity
    {
        public int UserId { get; set; }
        public int FriendId { get; set; }
        public string UserName { get; set; }

        public User User { get; set; }

    }
}
