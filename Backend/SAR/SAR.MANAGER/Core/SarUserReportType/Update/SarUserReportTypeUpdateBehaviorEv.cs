using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarUserReportType.Update
{
    class SarUserReportTypeUpdateBehaviorEv : BeanObjectBase, ISarUserReportTypeUpdate
    {
        SAR_USER_REPORT_TYPE entity;

        internal SarUserReportTypeUpdateBehaviorEv(CommonParam param, SAR_USER_REPORT_TYPE data)
            : base(param)
        {
            entity = data;
        }

        bool ISarUserReportTypeUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SarUserReportTypeDAO.Update(entity);
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
                result = result && SarUserReportTypeCheckVerifyValidData.Verify(param, entity);
                result = result && SarUserReportTypeCheckVerifyIsUnlock.Verify(param, entity.ID);
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
