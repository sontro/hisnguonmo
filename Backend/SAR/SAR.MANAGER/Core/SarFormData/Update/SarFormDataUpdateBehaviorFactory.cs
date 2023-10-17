using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarFormData.Update
{
    class SarFormDataUpdateBehaviorFactory
    {
        internal static ISarFormDataUpdate MakeISarFormDataUpdate(CommonParam param, object data)
        {
            ISarFormDataUpdate result = null;
            try
            {
                if (data.GetType() == typeof(SAR_FORM_DATA))
                {
                    result = new SarFormDataUpdateBehaviorEv(param, (SAR_FORM_DATA)data);
                }
                else if (data.GetType() == typeof(List<SAR_FORM_DATA>))
                {
                    result = new SarFormDataUpdateListBehaviorEv(param, (List<SAR_FORM_DATA>)data);
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
