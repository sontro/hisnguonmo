using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarReportType.Update
{
    class SarReportTypeUpdateBehaviorEv : BeanObjectBase, ISarReportTypeUpdate
    {
        SAR_REPORT_TYPE entity;

        internal SarReportTypeUpdateBehaviorEv(CommonParam param, SAR_REPORT_TYPE data)
            : base(param)
        {
            entity = data;
        }

        bool ISarReportTypeUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SarReportTypeDAO.Update(entity);
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
                result = result && SarReportTypeCheckVerifyValidData.Verify(param, entity);
                result = result && SarReportTypeCheckVerifyIsUnlock.Verify(param, entity.ID);
                result = result && SarReportTypeCheckVerifyExistsCode.Verify(param, entity.REPORT_TYPE_CODE, entity.ID);
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
