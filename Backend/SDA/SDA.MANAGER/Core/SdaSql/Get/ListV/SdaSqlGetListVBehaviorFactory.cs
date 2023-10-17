using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaSql.Get.ListV
{
    class SdaSqlGetListVBehaviorFactory
    {
        internal static ISdaSqlGetListV MakeISdaSqlGetListV(CommonParam param, object data)
        {
            ISdaSqlGetListV result = null;
            try
            {
                if (data.GetType() == typeof(SdaSqlViewFilterQuery))
                {
                    result = new SdaSqlGetListVBehaviorByViewFilterQuery(param, (SdaSqlViewFilterQuery)data);
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
