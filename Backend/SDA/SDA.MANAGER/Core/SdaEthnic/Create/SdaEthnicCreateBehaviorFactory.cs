using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaEthnic.Create
{
    class SdaEthnicCreateBehaviorFactory
    {
        internal static ISdaEthnicCreate MakeISdaEthnicCreate(CommonParam param, object data)
        {
            ISdaEthnicCreate result = null;
            try
            {
                if (data.GetType() == typeof(SDA_ETHNIC))
                {
                    result = new SdaEthnicCreateBehaviorEv(param, (SDA_ETHNIC)data);
                }
                else if (data.GetType() == typeof(List<SDA_ETHNIC>))
                {
                    result = new SdaEthnicCreateBehaviorListEv(param, (List<SDA_ETHNIC>)data);
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
