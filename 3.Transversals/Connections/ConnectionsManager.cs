namespace Transversals.Connections
{
    using Microsoft.Extensions.Configuration;
    using System.IO;

    public sealed class ConnectionsManager
    {
        private static ConnectionsManager _instance = null;
        private string? databaseConnection;
        private string? clave;
        private string? iv;

        private ConnectionsManager()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);
            var root = configurationBuilder.Build();
            databaseConnection = root.GetConnectionString("DatabaseConnection");
            clave = root.GetSection("JWT:Clave").Value; ;
            iv = root.GetSection("JWT:IV").Value; ;
        }
        public static ConnectionsManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ConnectionsManager();

                return _instance;
            }
        }

        public string? DatabaseConnection
        {
            get
            {
                return databaseConnection;
            }
        }

        public string? Clave
        {
            get
            {
                return clave;
            }
        }
        public string? IV
        {
            get
            {
                return iv;
            }
        }
    }
}
