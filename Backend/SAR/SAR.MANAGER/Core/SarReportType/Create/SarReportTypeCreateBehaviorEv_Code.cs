using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarReportType.Create
{
    class SarReportTypeCreateBehaviorEv : BeanObjectBase, ISarReportTypeCreate
    {
        SAR_REPORT_TYPE entity;

        internal SarReportTypeCreateBehaviorEv(CommonParam param, SAR_REPORT_TYPE data)
            : base(param)
        {
            entity = data;
        }

        bool ISarReportTypeCreate.Run()
        {
            bool result = false;
            try
            {
                if (entity.CREATOR == "")
                {
                    entity.CREATOR = null;
                }
                result = Check() && DAOWorker.SarReportTypeDAO.Create(entity);
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
                result = result && SarReportTypeCheckVerifyExistsCode.Verify(param, entity.REPORT_TYPE_CODE, null);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
    }
}
