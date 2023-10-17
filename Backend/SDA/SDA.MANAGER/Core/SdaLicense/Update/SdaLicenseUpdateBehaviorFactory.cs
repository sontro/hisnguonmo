using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using SDA.SDO;

namespace SDA.MANAGER.Core.SdaLicense.Update
{
    class SdaLicenseUpdateBehaviorFactory
    {
        internal static ISdaLicenseUpdate MakeISdaLicenseUpdate(CommonParam param, object data)
        {
            ISdaLicenseUpdate result = null;
            try
            {
                if (data.GetType() == typeof(SDA_LICENSE))
                {
                    result = new SdaLicenseUpdateBehaviorEv(param, (SDA_LICENSE)data);
                }
                else if (data.GetType() == typeof(SdaLicenseSDO))
                {
                    result = new SdaLicenseUpdateBehaviorEvSdo(param, (SdaLicenseSDO)data);
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
