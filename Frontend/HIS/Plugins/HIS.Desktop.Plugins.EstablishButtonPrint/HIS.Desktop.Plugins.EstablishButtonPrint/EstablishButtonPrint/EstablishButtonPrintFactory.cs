using Inventec.Core;
using System;

namespace HIS.Desktop.Plugins.EstablishButtonPrint
{
    class EstablishButtonPrintFactory
    {
        internal static IEstablishButtonPrint MakeIControl(CommonParam param, object[] data)
        {
            IEstablishButtonPrint result = null;
            try
            {               
                result = new HisBedTypeBehavior(param, data);
                
                if (result == null) throw new NullReferenceException();
            }
            catch (NullReferenceException ex)
            {
                Inventec.Common.Logging.LogSystem.Error("Factory khong khoi tao duoc doi tuong." + data.GetType().ToString() + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => data), data), ex);
                result = null;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}
