using AmazonScrapper.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonScrapper.Settings
{
    public static class Config
    {
        #region UserField
        public static Fields ReadUserFromFile()
        {
            return Utility.ReadFromFile<Fields>();
        }

        public static void WriteUserToFile(this Fields user)
        {
            Utility.WriteToFile(user);
        }

        public static Dictionary<string, UserConfig> GetUserDictionary()
        {
            var user = ReadUserFromFile();
            return user.ToDictionary();
        }
        #endregion

        #region Other Config
        public static OtherConfig ReadOtherConfigFromFile()
        {
            return Utility.ReadFromFile<OtherConfig>();
        }

        public static void WriteOtherConfigToFile(this OtherConfig user)
        {
            Utility.WriteToFile(user);
        }
        #endregion
    }
}
