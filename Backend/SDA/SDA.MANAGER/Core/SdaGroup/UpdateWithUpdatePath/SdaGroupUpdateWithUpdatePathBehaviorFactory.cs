using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaGroup.UpdateWithUpdatePath
{
    class SdaGroupUpdateWithUpdatePathBehaviorFactory
    {
        internal static ISdaGroupUpdateWithUpdatePath MakeISdaGroupUpdateWithUpdatePath(CommonParam param, object data)
        {
            ISdaGroupUpdateWithUpdatePath result = null;
            try
            {
                if (data.GetType() == typeof(SDA_GROUP))
                {
                    result = new SdaGroupUpdateWithUpdatePathBehavior(param, (SDA_GROUP)data);
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
