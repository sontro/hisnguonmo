using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarPrintTypeCfg.Update
{
    class SarPrintTypeCfgUpdateBehaviorFactory
    {
        internal static ISarPrintTypeCfgUpdate MakeISarPrintTypeCfgUpdate(CommonParam param, object data)
        {
            ISarPrintTypeCfgUpdate result = null;
            try
            {
                if (data.GetType() == typeof(SAR_PRINT_TYPE_CFG))
                {
                    result = new SarPrintTypeCfgUpdateBehaviorEv(param, (SAR_PRINT_TYPE_CFG)data);
                }
                else if (data.GetType() == typeof(List<SAR_PRINT_TYPE_CFG>))
                {
                    result = new SarPrintTypeCfgUpdateListBehaviorEv(param, (List<SAR_PRINT_TYPE_CFG>)data);
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
