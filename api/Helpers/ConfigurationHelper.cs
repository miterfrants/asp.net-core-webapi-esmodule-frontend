namespace Sample.Helpers
{
    public class AppSettings
    {
        public Secrets Secrets { get; set; }
        public Common Common { get; set; }
    }

    public class Common
    {
        public Auth Auth { get; set; }
    }

    public class Auth
    {
        public int JwtExpirationMonth { get; set; }
    }

    public class Secrets
    {
        public string DBConnectionString { get; set; }
        public string JwtKey { get; set; }
    }
}