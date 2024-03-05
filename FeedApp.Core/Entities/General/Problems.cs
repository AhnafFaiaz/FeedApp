using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeedApp.Core.Enum;

namespace FeedApp.Core.Entities.General
{
        [Table("Problems")]
        public class Problems : Base<int>
        {
        //[Required, StringLength(maximumLength: 8, MinimumLength = 1)]
        //public string? probID { get; set; }
            [StringLength(maximumLength: 50, MinimumLength = 2)]   
            public string Subject { get; set; }

            [Required, StringLength(maximumLength: 200, MinimumLength = 2)]
            public string ProblemDesc { get; set; }

            public Status Status { get; set; }

            [NotMapped]
            public string UserName { get; set; }

            public int? UserId { get; set; }
            
            [ForeignKey("UserId")]
            public virtual Users Users { get; set; }
    }
}



