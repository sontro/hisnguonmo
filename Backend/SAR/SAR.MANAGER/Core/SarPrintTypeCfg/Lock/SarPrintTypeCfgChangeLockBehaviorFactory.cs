using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarPrintTypeCfg.Lock
{
    class SarPrintTypeCfgChangeLockBehaviorFactory
    {
        internal static ISarPrintTypeCfgChangeLock MakeISarPrintTypeCfgChangeLock(CommonParam param, object data)
        {
            ISarPrintTypeCfgChangeLock result = null;
            try
            {
                if (data.GetType() == typeof(SAR_PRINT_TYPE_CFG))
                {
                    result = new SarPrintTypeCfgChangeLockBehaviorEv(param, (SAR_PRINT_TYPE_CFG)data);
                }
                else if (data.GetType() == typeof(SAR.SDO.ChangeLockByControlSDO))
                {
                    result = new SarPrintTypeCfgChangeLockByControlBehaviorEv(param, (SAR.SDO.ChangeLockByControlSDO)data);
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
