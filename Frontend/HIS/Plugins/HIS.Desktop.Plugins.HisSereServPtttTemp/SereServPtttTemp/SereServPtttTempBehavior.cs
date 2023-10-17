using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisSereServPtttTemp.SereServPtttTemp
{
    class SereServPtttTempBehavior: Tool<IDesktopToolContext>, ISereServPtttTemp
    {
        object[] entity;
        internal SereServPtttTempBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object ISereServPtttTemp.Run()
        {
            Inventec.Desktop.Common.Modules.Module moduleData = null;
            List<long> SERVICE_TYPE_IDs = null;
            long service_id = 0;
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is Inventec.Desktop.Common.Modules.Module)
                            moduleData = (Inventec.Desktop.Common.Modules.Module)item;
                        //else if (item is long)
                        //    service_id = (long)item;
                        //else if (item is List<long>)
                        //    SERVICE_TYPE_IDs = (List<long>)item;
                    }
                }
                if (moduleData != null)
                {
                    return new frmSereServPtttTemp();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}
