using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaConfigAppUser.Create
{
    class SdaConfigAppUserCreateBehaviorFactory
    {
        internal static ISdaConfigAppUserCreate MakeISdaConfigAppUserCreate(CommonParam param, object data)
        {
            ISdaConfigAppUserCreate result = null;
            try
            {
                if (data.GetType() == typeof(SDA_CONFIG_APP_USER))
                {
                    result = new SdaConfigAppUserCreateBehaviorEv(param, (SDA_CONFIG_APP_USER)data);
                }
                else if (data.GetType() == typeof(List<SDA_CONFIG_APP_USER>))
                {
                    result = new SdaConfigAppUserCreateBehaviorList(param, (List<SDA_CONFIG_APP_USER>)data);
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
