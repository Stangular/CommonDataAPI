using Microsoft.Extensions.Configuration;
using System;

namespace Configuration
{
    public class RedisConfigurationSection // : ConfigurationSection
    {
        #region Constants

        private const string HostAttributeName = "host";
        private const string PortAttributeName = "port";
        private const string PasswordAttributeName = "password";
        private const string DatabaseIDAttributeName = "databaseID";

        #endregion

        #region Properties

      //  [ConfigurationProperty(HostAttributeName, IsRequired = true)]
    //    public string Host
    //    {
    //        get { return this[HostAttributeName].ToString(); }
    //    }

    //  //  [ConfigurationProperty(PortAttributeName, IsRequired = true)]
    //    public int Port
    //    {
    //        get
    //        {
    //            int p;
    //            return Int32.TryParse(this[PortAttributeName], out p) ? p : -1;
 
    //        }
    //    }

    ////    [ConfigurationProperty(PasswordAttributeName, IsRequired = false)]
    //    public string Password
    //    {
    //        get { return this[PasswordAttributeName].ToString(); }
    //    }

    ////    [ConfigurationProperty(DatabaseIDAttributeName, IsRequired = false)]
    //    public long DatabaseID
    //    {
    //        get
    //        {
    //            long p;
    //            return Int64.TryParse(this[DatabaseIDAttributeName], out p) ? p : -1;

    //        }
    //    }

        #endregion
    }
}