using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA.MANAGER.Core.SdaProvince.Get.ListDynamic
{
    class SdaProvinceGetListDynamicBehaviorFactory
    {
        internal static ISdaProvinceGetListDynamic MakeISdaProvinceGetListDynamic(CommonParam param, object data)
        {
            ISdaProvinceGetListDynamic result = null;
            try
            {
                if (data.GetType() == typeof(SdaProvinceViewFilterQuery))
                {
                    result = new SdaProvinceGetListDynamicBehaviorByFilterQuery(param, (SdaProvinceViewFilterQuery)data);
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
