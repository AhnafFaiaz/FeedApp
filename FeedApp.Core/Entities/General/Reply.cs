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
    [Table("Reply")]
    public class Reply : Base<int>
    {
        [Required, StringLength(maximumLength: 200, MinimumLength = 2)]
        public string ReplyDesc { get; set; }

        [NotMapped]
        public Status Status { get; set; }

        [NotMapped]
        public List<Reply> Replies { get; set; }//Admin e AddReplies e previous reply dekhanor jonne add korsi

        //[NotMapped]
        //public Users Users { get; set; }
        //[NotMapped]
        //public Users UserName { get; set; }

        public int ProblemId { get; set; }

        [ForeignKey("ProblemId")]//ProblemId
        public virtual Problems Problems { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual Users Users { get; set; }
    }
}
