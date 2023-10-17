using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaProvince.Delete
{
    class SdaProvinceDeleteBehaviorFactory
    {
        internal static ISdaProvinceDelete MakeISdaProvinceDelete(CommonParam param, object data)
        {
            ISdaProvinceDelete result = null;
            try
            {
                if (data.GetType() == typeof(SDA_PROVINCE))
                {
                    result = new SdaProvinceDeleteBehaviorEv(param, (SDA_PROVINCE)data);
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
