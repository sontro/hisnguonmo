using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaReligion.Create
{
    class SdaReligionCreateBehaviorFactory
    {
        internal static ISdaReligionCreate MakeISdaReligionCreate(CommonParam param, object data)
        {
            ISdaReligionCreate result = null;
            try
            {
                if (data.GetType() == typeof(SDA_RELIGION))
                {
                    result = new SdaReligionCreateBehaviorEv(param, (SDA_RELIGION)data);
                }
                else if (data.GetType() == typeof(List<SDA_RELIGION>))
                {
                    result = new SdaReligionCreateBehaviorListEv(param, (List<SDA_RELIGION>)data);
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
