using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarFormType.Update
{
    class SarFormTypeUpdateBehaviorFactory
    {
        internal static ISarFormTypeUpdate MakeISarFormTypeUpdate(CommonParam param, object data)
        {
            ISarFormTypeUpdate result = null;
            try
            {
                if (data.GetType() == typeof(SAR_FORM_TYPE))
                {
                    result = new SarFormTypeUpdateBehaviorEv(param, (SAR_FORM_TYPE)data);
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