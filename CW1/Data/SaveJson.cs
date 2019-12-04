using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CW1.Data
{
    public static class SaveJson
    {
        public static string SerializeDictionaryToJsonString<TKey, TValue>(Dictionary<TKey, TValue> dic)
        {
            if (dic.Count == 0)
                return "";

            string jsonStr = JsonConvert.SerializeObject(dic);
            return jsonStr;
        }

        public static Dictionary<TKey, TValue> DeserializeStringToDictionary<TKey, TValue>(string jsonStr)
        {
            if (string.IsNullOrEmpty(jsonStr))
                return new Dictionary<TKey, TValue>();

            Dictionary<TKey, TValue> jsonDict = JsonConvert.DeserializeObject<Dictionary<TKey, TValue>>(jsonStr);

            return jsonDict;
        }
    }
}
