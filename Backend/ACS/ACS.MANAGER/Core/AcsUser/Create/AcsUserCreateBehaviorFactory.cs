using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsUser.Create
{
    class AcsUserCreateBehaviorFactory
    {
        internal static IAcsUserCreate MakeIAcsUserCreate(CommonParam param, object data)
        {
            IAcsUserCreate result = null;
            try
            {
                if (data.GetType() == typeof(ACS_USER))
                {
                    result = new AcsUserCreateBehaviorEv(param, (ACS_USER)data);
                }
                else if (data.GetType() == typeof(List<ACS_USER>))
                {
                    result = new AcsUserCreateBehaviorListEv(param, (List<ACS_USER>)data);
                }
                else if (data.GetType() == typeof(ACS.SDO.CreateAndGrantUserSDO))
                {
                    result = new AcsUserCreateBehaviorSdo(param, (ACS.SDO.CreateAndGrantUserSDO)data);
                }
                else if (data.GetType() == typeof(List<ACS.SDO.CreateAndGrantUserSDO>))
                {
                    result = new AcsUserCreateBehaviorListSdo(param, (List<ACS.SDO.CreateAndGrantUserSDO>)data);
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
