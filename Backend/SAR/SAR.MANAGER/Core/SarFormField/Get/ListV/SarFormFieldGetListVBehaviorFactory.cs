using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarFormField.Get.ListV
{
    class SarFormFieldGetListVBehaviorFactory
    {
        internal static ISarFormFieldGetListV MakeISarFormFieldGetListV(CommonParam param, object data)
        {
            ISarFormFieldGetListV result = null;
            try
            {
                if (data.GetType() == typeof(SarFormFieldViewFilterQuery))
                {
                    result = new SarFormFieldGetListVBehaviorByViewFilterQuery(param, (SarFormFieldViewFilterQuery)data);
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
