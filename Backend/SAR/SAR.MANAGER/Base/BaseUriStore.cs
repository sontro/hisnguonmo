using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAR.MANAGER.Base
{
    public class BaseUriStore
    {
        public static string GetFssUri = ConfigurationManager.AppSettings["fss.uri.base"];
    }
}
