using environment.net.core;
using Microsoft.Extensions.Configuration;

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

        IConfiguration configuration;

        private ConnectionInfo()
        {
            configuration = EnvironmentManager.Instance.GetConfiguration();
        }
    }
}
