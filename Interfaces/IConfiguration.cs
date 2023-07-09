namespace DevOpsBck.Interfaces
{
    public interface IConfiguration
    {
        AppSettings? Settings { get; }

        public string Credentials { get; }
    }
}
