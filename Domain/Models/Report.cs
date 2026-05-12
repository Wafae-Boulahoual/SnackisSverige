using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Report
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        public DateTime DateReport { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int? PostId { get; set; } // report en comment eller post
        public Post? Post { get; set; }
        public int? CommentId { get; set; }
        public Comment? Comment { get; set; }
        public bool IsReviewed { get; set; }
      
      
    }
}
