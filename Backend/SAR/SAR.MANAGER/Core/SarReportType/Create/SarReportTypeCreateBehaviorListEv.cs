using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarReportType.Create
{
    class SarReportTypeCreateBehaviorListEv : BeanObjectBase, ISarReportTypeCreate
    {
        List<SAR_REPORT_TYPE> entities;

        internal SarReportTypeCreateBehaviorListEv(CommonParam param, List<SAR_REPORT_TYPE> datas)
            : base(param)
        {
            entities = datas;
        }

        bool ISarReportTypeCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SarReportTypeDAO.CreateList(entities);
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
                result = result && SarReportTypeCheckVerifyValidData.Verify(param, entities);
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
