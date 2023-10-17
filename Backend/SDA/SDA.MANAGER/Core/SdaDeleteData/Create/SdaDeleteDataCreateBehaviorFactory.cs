using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaDeleteData.Create
{
    class SdaDeleteDataCreateBehaviorFactory
    {
        internal static ISdaDeleteDataCreate MakeISdaDeleteDataCreate(CommonParam param, object data)
        {
            ISdaDeleteDataCreate result = null;
            try
            {
                if (data.GetType() == typeof(SDA_DELETE_DATA))
                {
                    result = new SdaDeleteDataCreateBehaviorEv(param, (SDA_DELETE_DATA)data);
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
