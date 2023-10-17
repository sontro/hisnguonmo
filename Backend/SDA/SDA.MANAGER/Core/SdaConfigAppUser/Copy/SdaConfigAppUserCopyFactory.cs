using Inventec.Core;
using SDA.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA.MANAGER.Core.SdaConfigAppUser.Copy
{
    class SdaConfigAppUserCopyFactory
    {
        internal static ISdaConfigAppUserCopy MakeISarUserReportTypeCopy(CommonParam param, object data)
        {
            ISdaConfigAppUserCopy result = null;
            try
            {
                if (data.GetType() == typeof(SdaConfigAppUserCopyByUserSDO))
                {
                    result = new SdaConfigAppUserCopyByUserBehavior(param, (SdaConfigAppUserCopyByUserSDO)data);
                }
                else if (data.GetType() == typeof(SdaConfigAppUserCopyByConfigSDO))
                {
                    result = new SdaConfigAppUserCopyByConfigBehavior(param, (SdaConfigAppUserCopyByConfigSDO)data);
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
