using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarPrintLog.Create
{
    class SarPrintLogCreateBehaviorFactory
    {
        internal static ISarPrintLogCreate MakeISarPrintLogCreate(CommonParam param, object data)
        {
            ISarPrintLogCreate result = null;
            try
            {
                //if (data.GetType() == typeof(SAR_PRINT_LOG))
                //{
                //    result = new SarPrintLogCreateBehaviorEv(param, (SAR_PRINT_LOG)data);
                //}
                //else 
                if (data.GetType() == typeof(SDO.SarPrintLogSDO))
                {
                    result = new SarPrintLogCreateBehaviorSDO(param, (SDO.SarPrintLogSDO)data);
                }
                else if (data.GetType() == typeof(List<SDO.SarPrintLogSDO>))
                {
                    result = new SarPrintLogCreateListBehaviorSDO(param, (List<SDO.SarPrintLogSDO>)data);
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
