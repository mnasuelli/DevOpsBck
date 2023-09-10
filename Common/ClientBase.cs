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
                //string _credentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", "", _configuration.Settings?.PersonalAccessToken)));
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri($"{_configuration.Settings?.BaseUri}/{_configuration.Settings?.OrganizationName}/{_configuration.Settings?.ProjectName}");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _configuration.Credentials);
                //string credentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", "", _configuration.Settings?.PersonalAccessToken)));
                //Uri devOpsUri = new Uri($"{_configuration.Settings?.BaseUri}/{_configuration.Settings?.OrganizationName}/{_configuration.Settings?.ProjectName}");
                //HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(devOpsUri);
                return client;
            }
            return null;
        }
    }
}
