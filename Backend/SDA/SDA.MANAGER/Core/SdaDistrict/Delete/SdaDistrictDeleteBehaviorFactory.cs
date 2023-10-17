using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaDistrict.Delete
{
    class SdaDistrictDeleteBehaviorFactory
    {
        internal static ISdaDistrictDelete MakeISdaDistrictDelete(CommonParam param, object data)
        {
            ISdaDistrictDelete result = null;
            try
            {
                if (data.GetType() == typeof(SDA_DISTRICT))
                {
                    result = new SdaDistrictDeleteBehaviorEv(param, (SDA_DISTRICT)data);
                }
                else if (data.GetType() == typeof(List<long>))
                {
                    result = new SdaDistrictDeleteBehaviorByProvince(param, (List<long>)data);
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
