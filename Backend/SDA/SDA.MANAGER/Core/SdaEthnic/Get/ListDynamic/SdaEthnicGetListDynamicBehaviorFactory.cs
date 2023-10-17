using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA.MANAGER.Core.SdaEthnic.Get.ListDynamic
{
    class SdaEthnicGetListDynamicBehaviorFactory
    {
        internal static ISdaEthnicGetListDynamic MakeISdaEthnicGetListDynamic(CommonParam param, object data)
        {
            ISdaEthnicGetListDynamic result = null;
            try
            {
                if (data.GetType() == typeof(SdaEthnicFilterQuery))
                {
                    result = new SdaEthnicGetListDynamicBehaviorByFilterQuery(param, (SdaEthnicFilterQuery)data);
                }
                if (result == null) throw new NullReferenceException();
            }
            catch (NullReferenceException ex)
            {
                MANAGER.Base.BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.Common__FactoryKhoiTaoDoiTuongThatBai);
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
