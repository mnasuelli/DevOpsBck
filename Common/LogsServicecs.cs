namespace DevOpsBck.Common
{
    public static class LogsServicecs
    {
        private static string separator = ";";
        private static IConfiguration _configuration;
        private static StringBuilder logBuilder { get; set; }
        public static StringBuilder InitLogs()
        {
            Configuration configuration = new Configuration();  
            _configuration = configuration; 
            string filelogs = _configuration.Settings.DestinationPath + "logs.csv";
            if (File.Exists(filelogs))
            {
                File.Copy(filelogs, _configuration.Settings.DestinationPath + "logs_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".csv");
                File.Delete(filelogs);
            }
            logBuilder = new StringBuilder();
            string[] headings = { "Result", "Message", "Datetime", };
            logBuilder.AppendLine(string.Join(separator, headings));
            return logBuilder;
        }

        public static void AddLogs(string result, string message, string date)
        {
            string[] newrow = { result, message, date };
            logBuilder.AppendLine(string.Join(separator, newrow));
        }

        public static void CreateFileLogs()
        {
            string file = _configuration.Settings.DestinationPath + "logs.csv";
            File.AppendAllText(file, logBuilder.ToString());
        }
    }
}
