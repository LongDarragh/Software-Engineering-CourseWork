using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using CW1.Data;

namespace CW1
{
    public class Sms : IFilter
    {
        private string message;
        public Sms(string _message)
        {
            message = _message.Trim();
        }
        public Dictionary<string, string> StartFilter()
        {
            Dictionary<string, string> SMSInormation = new Dictionary<string, string>();
            try
            {
                int SenderLoc = message.IndexOf(' ');
                string Sender = message.Substring(0, SenderLoc);
                string Content = message.Substring(SenderLoc + 1, message.Length - SenderLoc - 1);
                SMSInormation.Add("Sender", Sender);
                SMSInormation.Add("Content", exchangeTool.Exchange(Content));
                //Console.WriteLine("SMS");
            }
            catch (Exception e)
            {
                Console.WriteLine("input error!\nerror Information:");
                Console.WriteLine(e);
            }
            return SMSInormation;
        }
    }
}
