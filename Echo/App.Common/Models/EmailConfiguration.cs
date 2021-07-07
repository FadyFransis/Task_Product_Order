namespace App.Common.Models
{
    public class EmailConfiguration
    {
        public string MailServer { get; set; }
        public string MailPort { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string SupportEmail { get; set; }
        public string AdminEmail { get; set; }
        public bool EnableSsl { get; set; }
    }
}
