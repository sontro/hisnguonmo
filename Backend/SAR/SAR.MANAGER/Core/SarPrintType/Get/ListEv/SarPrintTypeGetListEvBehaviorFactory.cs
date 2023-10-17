using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarPrintType.Get.ListEv
{
    class SarPrintTypeGetListEvBehaviorFactory
    {
        internal static ISarPrintTypeGetListEv MakeISarPrintTypeGetListEv(CommonParam param, object data)
        {
            ISarPrintTypeGetListEv result = null;
            try
            {
                if (data.GetType() == typeof(SarPrintTypeFilterQuery))
                {
                    result = new SarPrintTypeGetListEvBehaviorByFilterQuery(param, (SarPrintTypeFilterQuery)data);
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
