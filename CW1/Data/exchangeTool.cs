using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CW1.Data
{
    public static class exchangeTool
    {

        /*A method to exchange the textwords.csv
         * And exchange to the Dictonary
         */
        public static string Exchange(string message)
        {
            string FilePath = "../../Document/textwords.csv";
            List<string> textspeakList = new List<string>();
            Dictionary<string, string> textspeakDic = new Dictionary<string, string>();

            readCSV(FilePath, ref textspeakList, ref textspeakDic);

            foreach(string textspeak in textspeakList)
            {
                int InsertLoc = message.IndexOf(textspeak, 0, message.Length);
                while(InsertLoc != -1)
                {
                    int textspeakLen = textspeak.Length;
                    string inserContent = '<' + textspeakDic[textspeak] + '>';
                    int insertLen = inserContent.Length;

                    message = message.Insert(InsertLoc + textspeakLen, inserContent);

                    int SearchStartLoc = InsertLoc + textspeakLen + insertLen;
                    int SearchRestLen = message.Length - SearchStartLoc;

                    InsertLoc = message.IndexOf(textspeak, SearchStartLoc, SearchRestLen);
                }
            }
            return message;
        }


        /*
         *A method for read the textword.csv 
         */
        public static void readCSV(string path, ref List<string> textspeakList, ref Dictionary<string, string> textspeakDic)
        {
            var reader = new StreamReader(File.OpenRead(path));
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');

                textspeakList.Add(values[0]);
                textspeakDic.Add(values[0], values[1]);
            }
        }
    }
}
