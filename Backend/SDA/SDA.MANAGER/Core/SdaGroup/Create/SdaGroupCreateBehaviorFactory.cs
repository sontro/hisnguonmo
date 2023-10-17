using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaGroup.Create
{
    class SdaGroupCreateBehaviorFactory
    {
        internal static ISdaGroupCreate MakeISdaGroupCreate(CommonParam param, object data)
        {
            ISdaGroupCreate result = null;
            try
            {
                if (data.GetType() == typeof(SDA_GROUP))
                {
                    result = new SdaGroupCreateBehaviorEv(param, (SDA_GROUP)data);
                }
                else if (data.GetType() == typeof(List<SDA_GROUP>))
                {
                    result = new SdaGroupCreateBehaviorListEv(param, (List<SDA_GROUP>)data);
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
