using Microsoft.Extensions.Configuration;
using WFEngine.Environment;

namespace WFEngine.Core.Utilities
{
    public class ConnectionInfo
    {
        static volatile ConnectionInfo _instance;

        public static ConnectionInfo Instance
        {
            get
            {
                return _instance ?? (_instance = new ConnectionInfo());
            }
        }

        readonly IConfiguration configuration;

        private ConnectionInfo()
        {
            WFEnvironment environment = WFEnvironment.Instance;
            configuration = environment.GetConfiguration();
        }
        public string MySQLConnectionString => (string)configuration.GetValue(typeof(string),"mysql_connection");

        public string DesignerUrl => (string)configuration.GetValue(typeof(string), "web_url");

        public string RedisConnectionString => (string)configuration.GetValue(typeof(string), "redis_connection");
    }
}
