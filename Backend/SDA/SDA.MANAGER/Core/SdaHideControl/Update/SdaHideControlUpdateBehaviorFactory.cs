using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaHideControl.Update
{
    class SdaHideControlUpdateBehaviorFactory
    {
        internal static ISdaHideControlUpdate MakeISdaHideControlUpdate(CommonParam param, object data)
        {
            ISdaHideControlUpdate result = null;
            try
            {
                if (data.GetType() == typeof(SDA_HIDE_CONTROL))
                {
                    result = new SdaHideControlUpdateBehaviorEv(param, (SDA_HIDE_CONTROL)data);
                }
                else if (data.GetType() == typeof(List<SDA_HIDE_CONTROL>))
                {
                    result = new SdaHideControlUpdateListBehaviorEv(param, (List<SDA_HIDE_CONTROL>)data);
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
