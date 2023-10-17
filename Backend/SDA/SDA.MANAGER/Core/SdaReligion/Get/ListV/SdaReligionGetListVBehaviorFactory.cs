using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaReligion.Get.ListV
{
    class SdaReligionGetListVBehaviorFactory
    {
        internal static ISdaReligionGetListV MakeISdaReligionGetListV(CommonParam param, object data)
        {
            ISdaReligionGetListV result = null;
            try
            {
                if (data.GetType() == typeof(SdaReligionViewFilterQuery))
                {
                    result = new SdaReligionGetListVBehaviorByViewFilterQuery(param, (SdaReligionViewFilterQuery)data);
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
