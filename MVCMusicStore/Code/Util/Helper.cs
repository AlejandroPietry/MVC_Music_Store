using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCMusicStore.Code.Util
{
    public class Helper
    {
        public static string PasswordHash(string password)
        {
            int hash = password.GetHashCode();
            return $"{hash}{hash}ççale";
        }
    }
}
