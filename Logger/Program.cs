
namespace Logger
{
    public class LogIn
    {
        //string filepath = @"C:\Users\ssaxena\OneDrive - WatchGuard Technologies Inc\Desktop\LogFile.txt";
        public static void statment(string message)
        {
            StreamWriter writer = new StreamWriter(@"C:\Users\ssaxena\OneDrive - WatchGuard Technologies Inc\Desktop\LogFile.txt", true);
            writer.WriteLine("information: {0}", message);
            writer.Close();
        }
        public static void warnings(string message)
        {
            StreamWriter writer = new StreamWriter(@"C:\Users\ssaxena\OneDrive - WatchGuard Technologies Inc\Desktop\LogFile.txt", true);
            writer.WriteLine("warning: {0}", message);
            writer.Close();
        }
        public static void error(string message)
        {
            StreamWriter writer = new StreamWriter(@"C:\Users\ssaxena\OneDrive - WatchGuard Technologies Inc\Desktop\LogFile.txt", true);
            writer.WriteLine("error: {0}", message);
            writer.Close();
        }





        /*       public  static void read(Log log)
               {
                   Log.Logger = new LoggerConfiguration()
                                 .WriteTo.Console()
                                 .WriteTo.File(@"C:\Users\ssaxena\OneDrive - WatchGuard Technologies Inc\Desktop\LogFile.txt")
                                 .CreateLogger();
               }*/
        public static void Main(string[] args) {

           //LogIn log = new LogIn();


        }
    }
}
