using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;
using SAR.SDO;

namespace SAR.MANAGER.Core.SarForm.Create
{
    class SarFormCreateBehaviorFactory
    {
        internal static ISarFormCreate MakeISarFormCreate(CommonParam param, object data)
        {
            ISarFormCreate result = null;
            try
            {
                if (data.GetType() == typeof(SAR_FORM))
                {
                    result = new SarFormCreateBehaviorEv(param, (SAR_FORM)data);
                }
                else if (data.GetType() == typeof(SarFormCreateOrUpdateSDO))
                {
                    result = new SarFormCreateOrUpdateBehaviorEv(param, (SarFormCreateOrUpdateSDO)data);
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
