using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarForm.Get.ListV
{
    class SarFormGetListVBehaviorFactory
    {
        internal static ISarFormGetListV MakeISarFormGetListV(CommonParam param, object data)
        {
            ISarFormGetListV result = null;
            try
            {
                if (data.GetType() == typeof(SarFormViewFilterQuery))
                {
                    result = new SarFormGetListVBehaviorByViewFilterQuery(param, (SarFormViewFilterQuery)data);
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
