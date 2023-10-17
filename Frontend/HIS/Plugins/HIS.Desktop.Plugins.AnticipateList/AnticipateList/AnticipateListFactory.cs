using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventec.Desktop.Plugins.AnticipateList.AnticipateList
{
    class AnticipateListFactory
    {
        internal static IAnticipateList MakeIAnticipateList(CommonParam param, object[] data)
        {
            IAnticipateList result = null;
            try
            {
                //if (data.GetType() == typeof(HisDebateUserFilter))
                //{
                result = new AnticipateListBehavior(param, data);
                //}
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
