using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaSqlParam.Create
{
    class SdaSqlParamCreateBehaviorFactory
    {
        internal static ISdaSqlParamCreate MakeISdaSqlParamCreate(CommonParam param, object data)
        {
            ISdaSqlParamCreate result = null;
            try
            {
                if (data.GetType() == typeof(SDA_SQL_PARAM))
                {
                    result = new SdaSqlParamCreateBehaviorEv(param, (SDA_SQL_PARAM)data);
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
