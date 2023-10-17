using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarReportTypeGroup.Create
{
    class SarReportTypeGroupCreateBehaviorEv : BeanObjectBase, ISarReportTypeGroupCreate
    {
        SAR_REPORT_TYPE_GROUP entity;

        internal SarReportTypeGroupCreateBehaviorEv(CommonParam param, SAR_REPORT_TYPE_GROUP data)
            : base(param)
        {
            entity = data;
        }

        bool ISarReportTypeGroupCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SarReportTypeGroupDAO.Create(entity);
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
                result = result && SarReportTypeGroupCheckVerifyValidData.Verify(param, entity);
                result = result && SarReportTypeGroupCheckVerifyExistsCode.Verify(param, entity.REPORT_TYPE_GROUP_CODE, null);
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
