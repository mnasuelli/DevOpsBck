using System.Net;

namespace DevOpsBck.Common
{
    public class ClientBase : HttpClient
    {
        IConfiguration _configuration;
        public ClientBase(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public HttpClient? GetClient()
        {
            if (_configuration != null)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri($"{_configuration.Settings?.BaseUri}/{_configuration.Settings?.OrganizationName}/{_configuration.Settings?.ProjectName}");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _configuration.Credentials);
                return client;
            }
            return null;
        }
    }
}
