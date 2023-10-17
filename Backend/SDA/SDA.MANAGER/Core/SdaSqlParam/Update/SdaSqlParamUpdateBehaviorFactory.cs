using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaSqlParam.Update
{
    class SdaSqlParamUpdateBehaviorFactory
    {
        internal static ISdaSqlParamUpdate MakeISdaSqlParamUpdate(CommonParam param, object data)
        {
            ISdaSqlParamUpdate result = null;
            try
            {
                if (data.GetType() == typeof(SDA_SQL_PARAM))
                {
                    result = new SdaSqlParamUpdateBehaviorEv(param, (SDA_SQL_PARAM)data);
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
