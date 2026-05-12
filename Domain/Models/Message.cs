using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TheMessage { get; set; }
        public DateTime SendingTime { get; set; }
        public string SenderId { get; set; }
        public ApplicationUser Sender { get; set; }
        public string ReceiverId { get; set; }
        public ApplicationUser Receiver { get; set; }
        public bool IsRead { get; set; } = false;
    }
}
