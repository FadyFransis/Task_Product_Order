using System.Collections.Generic;

namespace App.Common.Models
{
    public class EmailDTO
    {
        public List<string> MailTo { get; set; }
        public List<string> CC { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsBodyHTML { get; set; }
        public EmailConfiguration Configuration { get; set; }
    }
}
