using FeedApp.Core.Entities.General;
using FeedApp.Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedApp.Core.Entities.Business
{
    public class ProblemViewModel 
    {
        [Key]
        //[Required, StringLength(maximumLength: 8, MinimumLength = 2)]
        //public int problemID { get; set; }
        public int Id { get; set; }

        [StringLength(maximumLength: 50, MinimumLength = 2)]
        public string Subject { get; set; }

        [Required, StringLength(maximumLength: 200, MinimumLength = 2)]
        public string ProblemDesc { get; set; } = string.Empty;

        public int? UserID { get; set; }
       
        public Users Users { get; set; }

        //public int ReplyID { get; set; }

        public Status Status { get; set; }

        //public string UserName { get; set; }

        //[NotMapped]
        //public bool isSolved { get; set; }
    }
}
