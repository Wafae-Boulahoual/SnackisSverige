using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int PostId { get; set; }
        [JsonIgnore]
        public Post Post { get; set; }
        public DateTime DateAnswer { get; set; }
        public string Text { get; set; }
    }
}
