using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImpMestCreate.Base
{
    class StaticMethod
    {
        public static string GetTypeKey(long typeId, string groupCode,long? id = null)
        {
            if (id == null)
            {
                return string.Format("{0}_{1}", typeId, groupCode);
            }
            else
            {
                return string.Format("{0}_{1}_{2}", typeId, groupCode, id);
            }
        }

    }
}
