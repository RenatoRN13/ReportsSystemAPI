namespace ReportsSystemApi.Domain
{
    public class TokenConfig
    {
        public string audience { get; set; }
        public string issuer { get; set; }
        public int seconds { get; set; }
    }
}
