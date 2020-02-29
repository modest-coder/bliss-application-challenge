namespace Business.Settings
{
    public class EmailSettings
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string CredentialEmail { get; set; }
        public string CredentialPassword { get; set; }
        public string FromEmail { get; set; }
    }
}
