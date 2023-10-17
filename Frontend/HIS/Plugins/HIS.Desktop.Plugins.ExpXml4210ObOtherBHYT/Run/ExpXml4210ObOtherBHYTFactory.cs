using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpXml4210ObOtherBHYT.Run
{
    class ExpXml4210ObOtherBHYTFactory
    {
        internal static IExpXml4210ObOtherBHYT MakeIExpXml4210ObOtherBHYT(CommonParam param, object[] data)
        {
            IExpXml4210ObOtherBHYT result = null;
            try
            {
                result = new ExpXml4210ObOtherBHYTBehavior(param, data);
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
