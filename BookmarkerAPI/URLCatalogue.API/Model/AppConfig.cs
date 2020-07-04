namespace URLCatalogue.API.Model
{
    public class AppConfig
    {
        public string Environment { get; set; }
        public DatabaseSettings Data { get; set; }
        public AzureSettings AzureAd { get; set; }
        public string GraphApiUrl { get; set; }
    }

    public class DatabaseSettings
    {
        public EnvironmentDatabaseSettings DEV { get; set; }
        public EnvironmentDatabaseSettings UAT { get; set; }
        public EnvironmentDatabaseSettings PROD { get; set; }
    }

    public class EnvironmentDatabaseSettings
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string AuthType { get; set; }
        public string Database { get; set; }
        public string Server { get; set; }
    }

    public class AzureSettings
    {
        public string Instance { get; set; }
        public string Domain { get; set; }
        public string TenantId { get; set; }
        public string ClientId { get; set; }
        public string CallbackPath { get; set; }
        public string SignedOutCallbackPath { get; set; }
        public string ClientSecret { get; set; }
    }
}
