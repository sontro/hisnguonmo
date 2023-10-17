using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaLicense.Create
{
    class SdaLicenseCreateBehaviorFactory
    {
        internal static ISdaLicenseCreate MakeISdaLicenseCreate(CommonParam param, object data)
        {
            ISdaLicenseCreate result = null;
            try
            {
                if (data.GetType() == typeof(SDA_LICENSE))
                {
                    result = new SdaLicenseCreateBehaviorEv(param, (SDA_LICENSE)data);
                }
                else if (data.GetType() == typeof(List<SDA_LICENSE>))
                {
                    result = new SdaLicenseCreateBehaviorListEv(param, (List<SDA_LICENSE>)data);
                }
                else if (data.GetType() == typeof(string))
                {
                    result = new SdaLicenseCreateBehaviorString(param, (string)data);
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
