using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarRetyFofi.Create
{
    class SarRetyFofiCreateBehaviorFactory
    {
        internal static ISarRetyFofiCreate MakeISarRetyFofiCreate(CommonParam param, object data)
        {
            ISarRetyFofiCreate result = null;
            try
            {
                if (data.GetType() == typeof(SAR_RETY_FOFI))
                {
                    result = new SarRetyFofiCreateBehaviorEv(param, (SAR_RETY_FOFI)data);
                }
                else if (data.GetType() == typeof(List<SAR_RETY_FOFI>))
                {
                    result = new SarRetyFofiCreateBehaviorListEv(param, (List<SAR_RETY_FOFI>)data);
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
