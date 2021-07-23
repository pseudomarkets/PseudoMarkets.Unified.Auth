namespace PseudoMarkets.Unified.AuthLib.Models
{

    public class Config
    {
        public DataAccessConfig AuthDataAccessConfig { get; set; }
        public TokenConfig AuthTokenConfig { get; set; }
    }

    public class TokenConfig
    {
        public string TokenSecretKey { get; set; }
        public string TokenIssuer { get; set; }
        public string ServerId { get; set; }
    }

    public class DataAccessConfig
    {
        public AuthDataSource DataSource { get; set; }
        public RealTimeDs RealTimeDataSource { get; set; }
        public RelationalDs RelationalDataSource { get; set; }
    }

    public class RealTimeDs
    {
        public string Hostname { get; set; }
        public int Port { get; set; }
    }

    public class RelationalDs
    {
        public string ConnectionString { get; set; }
    }

    
}
