using HIS.Desktop.ADO;
using HIS.Desktop.Plugins.ExpMestViewDetail.ExpMestViewDetail.ExpMestViewDetail;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpMestViewDetail.ExpMestViewDetail
{
    class ExpMestViewDetailFactory
    {
        internal static IExpMestViewDetail MakeIExpMestView(CommonParam param, object[] data)
        {
            IExpMestViewDetail result = null;
            try
            {
                result = new ExpMestViewDetailBehavior(param, data);
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
