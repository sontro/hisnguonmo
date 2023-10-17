using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaNational.Create
{
    class SdaNationalCreateBehaviorFactory
    {
        internal static ISdaNationalCreate MakeISdaNationalCreate(CommonParam param, object data)
        {
            ISdaNationalCreate result = null;
            try
            {
                if (data.GetType() == typeof(SDA_NATIONAL))
                {
                    result = new SdaNationalCreateBehaviorEv(param, (SDA_NATIONAL)data);
                }
                else if (data.GetType() == typeof(List<SDA_NATIONAL>))
                {
                    result = new SdaNationalCreateBehaviorListEv(param, (List<SDA_NATIONAL>)data);
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
