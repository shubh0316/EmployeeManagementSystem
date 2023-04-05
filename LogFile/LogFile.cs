
using System;
namespace Logger
{
    public  class LogFiles
    {
        private DateTime datetime;
        private string _type;
        private string _message;
        public LogFiles(DateTime datetime,string type,string message)
        {
            this.datetime = datetime;
            this._type = type;
            this._message = message;
        }
        public override string ToString()
        {
            string statements =  $"DateTime: {datetime} Type: {_type}  Message : {_message}\n";
            return statements;
        }
        
    }
}
