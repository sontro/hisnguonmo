using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaSqlParam.Delete
{
    class SdaSqlParamDeleteBehaviorFactory
    {
        internal static ISdaSqlParamDelete MakeISdaSqlParamDelete(CommonParam param, object data)
        {
            ISdaSqlParamDelete result = null;
            try
            {
                if (data.GetType() == typeof(SDA_SQL_PARAM))
                {
                    result = new SdaSqlParamDeleteBehaviorEv(param, (SDA_SQL_PARAM)data);
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
