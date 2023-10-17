using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaConfigApp.Update
{
    class SdaConfigAppUpdateBehaviorFactory
    {
        internal static ISdaConfigAppUpdate MakeISdaConfigAppUpdate(CommonParam param, object data)
        {
            ISdaConfigAppUpdate result = null;
            try
            {
                if (data.GetType() == typeof(SDA_CONFIG_APP))
                {
                    result = new SdaConfigAppUpdateBehaviorEv(param, (SDA_CONFIG_APP)data);
                }
                else if (data.GetType() == typeof(List<SDA_CONFIG_APP>))
                {
                    result = new SdaConfigAppUpdateBehaviorList(param, (List<SDA_CONFIG_APP>)data);
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
