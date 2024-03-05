using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedApp.Core.Entities.General
{
    public class Users
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public int RoleID { get; set; }
        [ForeignKey("RoleID")]
        public virtual Roles Roles { get; set; }

    }
}
