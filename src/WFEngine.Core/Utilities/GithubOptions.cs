
using Microsoft.Extensions.Configuration;
using WFEngine.Environment;

namespace WFEngine.Core.Utilities
{
    public class GithubOptions
    {
        static GithubOptions _instance;

        public static GithubOptions Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GithubOptions();
                return _instance;
            }
        }

        public string ClientId => (string)configuration.GetValue(typeof(string), "github_client_id");

        public string RedirectUrl => (string)configuration.GetValue(typeof(string), "github_redirect_url");

        public string Login => (string)configuration.GetValue(typeof(string), "github_login");

        public string Scope => (string)configuration.GetValue(typeof(string), "github_scope");

        public string State => (string)configuration.GetValue(typeof(string), "github_state");

        public string ClientSecret => (string)configuration.GetValue(typeof(string), "github_client_secret");

        public string AllowSignUp => "true";

        readonly IConfiguration configuration;

        private GithubOptions()
        {
            WFEnvironment environmentManager = WFEnvironment.Instance;
            configuration = environmentManager.GetConfiguration();
        }
    }
}
