namespace softgen
{
    public static class Logger
    {
        private static string logFilePath = "application.log";

        public static void Log(string message)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine($"{DateTime.Now}: {message}");
                }
            }
            catch (Exception ex)
            {
                // If logging fails, there is not much we can do
                MessageBox.Show("Logging failed: " + ex.Message);
            }
        }
    }
}
