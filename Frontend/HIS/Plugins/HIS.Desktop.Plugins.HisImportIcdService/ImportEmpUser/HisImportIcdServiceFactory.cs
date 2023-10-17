using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;

namespace HIS.Desktop.Plugins.HisImportIcdService.ImportIcdService.Run
{
    class HisImportIcdServiceFactory
    {
        internal static IHisImportIcdService MakeIHisImportIcdService(CommonParam param, object[] data)
        {
            IHisImportIcdService result = null;
            try
            {
                result = new HisImportIcdServiceBehavior(param, data);
                if (result == null) throw new NullReferenceException();

            }
            catch (NullReferenceException ex)
            {
                Inventec.Common.Logging.LogSystem.Error(" Factory khong khoi tao duoc doi tuong." + data.GetType().ToString() + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => data), data), ex);
                result = null;

            }
            catch (Exception ex)
            {

                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }
    }
}
