using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaGroupType.Create
{
    class SdaGroupTypeCreateBehaviorFactory
    {
        internal static ISdaGroupTypeCreate MakeISdaGroupTypeCreate(CommonParam param, object data)
        {
            ISdaGroupTypeCreate result = null;
            try
            {
                if (data.GetType() == typeof(SDA_GROUP_TYPE))
                {
                    result = new SdaGroupTypeCreateBehaviorEv(param, (SDA_GROUP_TYPE)data);
                }
                else if (data.GetType() == typeof(List<SDA_GROUP_TYPE>))
                {
                    result = new SdaGroupTypeCreateBehaviorListEv(param, (List<SDA_GROUP_TYPE>)data);
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
