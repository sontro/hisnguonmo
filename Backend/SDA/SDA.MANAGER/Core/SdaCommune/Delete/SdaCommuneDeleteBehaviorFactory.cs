using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaCommune.Delete
{
    class SdaCommuneDeleteBehaviorFactory
    {
        internal static ISdaCommuneDelete MakeISdaCommuneDelete(CommonParam param, object data)
        {
            ISdaCommuneDelete result = null;
            try
            {
                if (data.GetType() == typeof(SDA_COMMUNE))
                {
                    result = new SdaCommuneDeleteBehaviorEv(param, (SDA_COMMUNE)data);
                }
                else if (data.GetType() == typeof(List<long>))
                {
                    result = new SdaCommuneDeleteBehaviorByDistrict(param, (List<long>)data);
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
