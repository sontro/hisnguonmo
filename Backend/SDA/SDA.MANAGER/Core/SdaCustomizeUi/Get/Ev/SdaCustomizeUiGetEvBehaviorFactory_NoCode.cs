using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaCustomizeUi.Get.Ev
{
    class SdaCustomizeUiGetEvBehaviorFactory
    {
        internal static ISdaCustomizeUiGetEv MakeISdaCustomizeUiGetEv(CommonParam param, object data)
        {
            ISdaCustomizeUiGetEv result = null;
            try
            {
                if (data.GetType() == typeof(long))
                {
                    result = new SdaCustomizeUiGetEvBehaviorById(param, long.Parse(data.ToString()));
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
