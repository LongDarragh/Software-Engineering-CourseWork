using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace CW1
{
    public class email : IFilter
    {
        private string message;
        public email(string _message)
        {
            message = _message.Trim();
        }
        public Dictionary<string, string> StartFilter()
        {
            Dictionary<string, string> EmailInormation = new Dictionary<string, string>();
            List<string> urlSet;
            try
            {
                //sender identification
                Regex senderReg = new Regex("[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+(\\.[a-zA-Z0-9_-]+)+");
                MatchCollection senderMatch = senderReg.Matches(message);
                //subject identification
                Regex subReg = new Regex(@"SIR [0-9]{2} / [0-9]{2} / [0-9]{2}");
                MatchCollection subMatch = subReg.Matches(message);
                if (subMatch.Count >= 1)
                {
                    EmailInormation.Add("EmailType", "Significant");
                    urlSet = ExchangeUrl(ref message);
                    urlSet.ForEach(Print);
                    Console.WriteLine(message);

                    //sport center code identification
                    Regex codeReg = new Regex(@"[0-9]{2}-[0-9]{3}-[0-9]{2}");
                    MatchCollection codeMatch = codeReg.Matches(message);

                    //Nature of Incident identification and add to dictionary
                    Regex incidentReg = new Regex(@"Nature of Incident");
                    MatchCollection incidentMatch;
                    var section = message.Split('\n');
                    for (int i = 0; i < section.Length; i++)
                    {
                        incidentMatch = incidentReg.Matches(section[i]);
                        if (incidentMatch.Count == 1)
                        {
                            string word = section[i].Substring(19, section[i].Length - 19);
                            EmailInormation.Add("Incident", word);
                            break;
                        }
                    }
                    //add subject and code to dictionary
                    EmailInormation.Add("Sender", senderMatch[0].Value);
                    EmailInormation.Add("Subject", subMatch[0].Value);
                    EmailInormation.Add("Code", codeMatch[0].Value);
                    EmailInormation.Add("URL", String.Join(";", urlSet.ToArray()));
                    //Console.WriteLine(EmailInormation["Sender"]);
                    //search content
                    int content = message.IndexOf(EmailInormation["Incident"]);
                    int len = EmailInormation["Incident"].Length;
                    int startLoc = content + len + 1;
                    int contentLen = message.Length - startLoc;
                    //Console.WriteLine(message.Substring(startLoc, contentLen));
                    EmailInormation.Add("Content", message.Substring(startLoc, contentLen));
                }
                else
                {
                    urlSet = ExchangeUrl(ref message);
                    EmailInormation.Add("EmailType", "Standard");
                    EmailInormation.Add("Sender", senderMatch[0].Value);
                    EmailInormation.Add("URL", String.Join(";", urlSet.ToArray()));
                    EmailInormation.Add("Content", message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("input error!\nerror Information:");
                Console.WriteLine(e);
            }
            return EmailInormation;
        }
        private List<string> ExchangeUrl(ref string outMessage)
        {
            List<string> urlSet = new List<string>();
            var words = message.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                //url identification
                Regex reg = new Regex(@"(?<do>http://[^/])(?<dir>([^/]+/)*([^/.]*$)?)((?<page>[^?.]+\.[^?]+)\?)?(?<par>.*$)");
                MatchCollection mc = reg.Matches(words[i]);
                Regex reg1 = new Regex(@"(?<do>www.[^/])(?<dir>([^/]+/)*([^/.]*$)?)((?<page>[^?.]+\.[^?]+)\?)?(?<par>.*$)");
                MatchCollection mc1 = reg1.Matches(words[i]);
                //url exchange
                if (mc.Count == 1 || mc1.Count == 1)
                {
                    urlSet.Add(words[i]);
                    words[i] = "<URL Quarantined>";
                }
            }
            //build message
            message = String.Join(" ", words);
            return urlSet;
        }
        //print function
        private static void Print(string str)
        {
            Console.WriteLine(str);
        }
    }
}