using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaTranslate.Create
{
    class SdaTranslateCreateBehaviorFactory
    {
        internal static ISdaTranslateCreate MakeISdaTranslateCreate(CommonParam param, object data)
        {
            ISdaTranslateCreate result = null;
            try
            {
                if (data.GetType() == typeof(SDA_TRANSLATE))
                {
                    result = new SdaTranslateCreateBehaviorEv(param, (SDA_TRANSLATE)data);
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
