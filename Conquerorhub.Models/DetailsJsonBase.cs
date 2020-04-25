using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conquerorhub.Models
{
   public class DetailsJsonBase
    {
        public static T LoadFromString<T>(string jsonDetails)
        {
            if (string.IsNullOrEmpty(jsonDetails))
                return default(T);

            return (T)JsonConvert.DeserializeObject(jsonDetails, typeof(T));
        }

        public static string SaveToString<T>(T value)
        {
            if (value == null)
                return null;

            return JsonConvert.SerializeObject(value);
        }

        public static object LoadFromString<T>(object detailsJSON)
        {
            throw new NotImplementedException();
        }
    }
}
