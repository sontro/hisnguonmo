using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarPrint.Create
{
    class SarPrintCreateBehaviorFactory
    {
        internal static ISarPrintCreate MakeISarPrintCreate(CommonParam param, object data)
        {
            ISarPrintCreate result = null;
            try
            {
                if (data.GetType() == typeof(SAR_PRINT))
                {
                    result = new SarPrintCreateBehaviorEv(param, (SAR_PRINT)data);
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
