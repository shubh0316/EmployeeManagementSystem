using System.IO;
namespace Logger
{
    public class LoggedData
    {
        private string _filepath;

        public LoggedData(string filepath)
        {
            _filepath = filepath;
        }
        public void WriteAllData(LogFiles data)
        {
            var text = File.ReadAllText(_filepath);
            File.WriteAllText(_filepath, data.ToString() + text);
        }
    }
}
