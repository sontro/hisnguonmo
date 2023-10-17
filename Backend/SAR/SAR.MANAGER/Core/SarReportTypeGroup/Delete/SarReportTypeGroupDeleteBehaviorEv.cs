using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarReportTypeGroup.Delete
{
    class SarReportTypeGroupDeleteBehaviorEv : BeanObjectBase, ISarReportTypeGroupDelete
    {
        SAR_REPORT_TYPE_GROUP entity;

        internal SarReportTypeGroupDeleteBehaviorEv(CommonParam param, SAR_REPORT_TYPE_GROUP data)
            : base(param)
        {
            entity = data;
        }

        bool ISarReportTypeGroupDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SarReportTypeGroupDAO.Truncate(entity);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        bool Check()
        {
            bool result = true;
            try
            {
                result = result && SarReportTypeGroupCheckVerifyIsUnlock.Verify(param, entity.ID, ref entity);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
