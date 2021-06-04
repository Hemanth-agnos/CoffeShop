using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class JsonExtensions
    {
        public static string ToJson(this object value)
        {
            if (value == null) return null;
            string json = JsonConvert.SerializeObject(value);
            return json;
        }
        public static T FromJson<T>(this string obj)
        {
            return !string.IsNullOrEmpty(obj) ? JsonConvert.DeserializeObject<T>(obj) : default;
        }
    }
}
