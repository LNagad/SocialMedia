using Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class User : AuditableBaseEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string ProfileImg { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Enabled { get; set; }
        public string? ActivationKey { get; set; }

        public ICollection<Post>? Posts { get; set; }
        public ICollection<Friends>? Friends { get; set; }
    }
}
