using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CW1.Data;


namespace CW1
{
    public class tweet : IFilter
    {
        private string message;
        public tweet(string _message)
        {
            message = _message.Trim();
        }
        public Dictionary<string, string> StartFilter()
        {
            Dictionary<string, string> TweetsInformation = new Dictionary<string, string>();

            try
            {
                message = exchangeTool.Exchange(message);
                Regex senderReg = new Regex("@[a-zA-Z0-9_-]+");
                MatchCollection senderMatch = senderReg.Matches(message);
                Regex tagReg = new Regex("#[a-zA-Z0-9_-]+");
                MatchCollection tagMatch = tagReg.Matches(message);
                string tagSet, mentionSet;

                List<string> tagList = new List<string>();
                foreach (Match tag in tagMatch)
                {
                    tagList.Add(tag.Value);
                }
                tagSet = String.Join(";", tagList);

                List<string> mentionList = new List<string>();
                for (int i = 1; i < senderMatch.Count; i++)
                {
                    mentionList.Add(senderMatch[i].Value);
                }
                mentionSet = String.Join(";", mentionList);

                TweetsInformation.Add("Sender", senderMatch[0].Value);
                TweetsInformation.Add("Hashtags", tagSet);
                TweetsInformation.Add("Mentions", mentionSet);
                TweetsInformation.Add("Content", message);
                Console.WriteLine(mentionSet);
            }
            catch (Exception e)
            {
                Console.WriteLine("input error!\nerror Information:");
                Console.WriteLine(e);
            }
            return TweetsInformation;
        }
    }
}
