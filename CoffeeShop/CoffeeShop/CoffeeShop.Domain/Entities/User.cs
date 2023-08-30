using Microsoft.AspNetCore.Identity;
using CoffeeShop.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Domain.Entities
{
    public class User : IdentityUser
    {
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public EntityStatus Status { get; set; } = EntityStatus.Active;
    }
}
