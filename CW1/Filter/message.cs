using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CW1;
using CW1.Data;
using System.Windows;

namespace CW1
{
    public class Message
    {
        protected string message;
        public IFilter filter;

        public Message(string _message)
        {
            message = _message.Trim();
        }
        public Dictionary<string, string> StartFilter()
        {
            Dictionary<string, string> msg = SaveJson.DeserializeStringToDictionary<string, string>(message);
            string header = msg["Header"];
            string body = msg["Body"];
            try
            {
                string headerID = header.Trim()[0].ToString();
                string headerAccount = header.Trim().Substring(1, 9);
                switch (header.Trim()[0])
                {
                    case 'S':
                        filter = new Sms(body);
                        Dictionary<string, string> sfInfomation = filter.StartFilter();
                        msg = msg.Concat(sfInfomation).ToDictionary(k => k.Key, v => v.Value);
                        msg.Add("Type", "SMS");
                        msg.Add("error", "0");
                        break;
                    case 'E':
                        filter = new email(body);
                        Dictionary<string, string> efInfomation = filter.StartFilter();
                        msg = msg.Concat(efInfomation).ToDictionary(k => k.Key, v => v.Value);
                        msg.Add("Type", "Email");
                        msg.Add("error", "0");
                        break;
                    case 'T':
                        filter = new tweet(body);
                        Dictionary<string, string> tfInfomation = filter.StartFilter();
                        msg = msg.Concat(tfInfomation).ToDictionary(k => k.Key, v => v.Value);
                        msg.Add("Type", "Tweets");
                        msg.Add("error", "0");
                        break;
                    default:
                        MessageBox.Show("Input Error");
                        msg.Add("error", "1");
                        break;
                }
            }
            catch (Exception e)
            {
                msg.Add("error", "1");
                msg.Add("errorInf", e.ToString());
            }
            return msg;
        }
    }
}
