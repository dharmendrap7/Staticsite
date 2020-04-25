using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conquerorhub.Repository
{
    public static class Settings
    {
        public static string ApiUserName => "conquerorhubapiuser";

        public static string ApiPassword => "sadada";

        public static string ApiUrlBase => "http://3.6.116.135:8010/api";
       // public static string ApiUrlBase => "http://3.6.10.123:8010/api";

        //public static string ApiUrlBase => "https://clientapi.mickrex.com/api";

        internal static bool UseMockData => true;
    }
}
