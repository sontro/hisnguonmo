using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaLanguage.Create
{
    class SdaLanguageCreateBehaviorFactory
    {
        internal static ISdaLanguageCreate MakeISdaLanguageCreate(CommonParam param, object data)
        {
            ISdaLanguageCreate result = null;
            try
            {
                if (data.GetType() == typeof(SDA_LANGUAGE))
                {
                    result = new SdaLanguageCreateBehaviorEv(param, (SDA_LANGUAGE)data);
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
