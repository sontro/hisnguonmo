using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaModuleField.Lock
{
    class SdaModuleFieldChangeLockBehaviorFactory
    {
        internal static ISdaModuleFieldChangeLock MakeISdaModuleFieldChangeLock(CommonParam param, object data)
        {
            ISdaModuleFieldChangeLock result = null;
            try
            {
                if (data.GetType() == typeof(SDA_MODULE_FIELD))
                {
                    result = new SdaModuleFieldChangeLockBehaviorEv(param, (SDA_MODULE_FIELD)data);
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
