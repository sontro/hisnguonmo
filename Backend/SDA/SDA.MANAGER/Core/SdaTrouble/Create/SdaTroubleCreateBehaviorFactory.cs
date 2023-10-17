using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaTrouble.Create
{
    class SdaTroubleCreateBehaviorFactory
    {
        internal static ISdaTroubleCreate MakeISdaTroubleCreate(CommonParam param, object data)
        {
            ISdaTroubleCreate result = null;
            try
            {
                if (data.GetType() == typeof(SDA_TROUBLE))
                {
                    result = new SdaTroubleCreateBehaviorEv(param, (SDA_TROUBLE)data);
                }
                else if (data.GetType() == typeof(List<SDA_TROUBLE>))
                {
                    result = new SdaTroubleCreateBehaviorListEv(param, (List<SDA_TROUBLE>)data);
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
