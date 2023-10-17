using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarReportTypeGroup.Update
{
    class SarReportTypeGroupUpdateBehaviorEv : BeanObjectBase, ISarReportTypeGroupUpdate
    {
        SAR_REPORT_TYPE_GROUP current;
        SAR_REPORT_TYPE_GROUP entity;

        internal SarReportTypeGroupUpdateBehaviorEv(CommonParam param, SAR_REPORT_TYPE_GROUP data)
            : base(param)
        {
            entity = data;
        }

        bool ISarReportTypeGroupUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SarReportTypeGroupDAO.Update(entity);
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
                result = result && SarReportTypeGroupCheckVerifyIsUnlock.Verify(param, entity.ID, ref current);
                result = result && SarReportTypeGroupCheckVerifyExistsCode.Verify(param, entity.REPORT_TYPE_GROUP_CODE, entity.ID);
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
