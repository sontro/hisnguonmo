using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaLicense.Get.ListV
{
    class SdaLicenseGetListVBehaviorFactory
    {
        internal static ISdaLicenseGetListV MakeISdaLicenseGetListV(CommonParam param, object data)
        {
            ISdaLicenseGetListV result = null;
            try
            {
                if (data.GetType() == typeof(SdaLicenseViewFilterQuery))
                {
                    result = new SdaLicenseGetListVBehaviorByViewFilterQuery(param, (SdaLicenseViewFilterQuery)data);
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
