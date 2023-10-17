using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarReportStt.Create
{
    class SarReportSttCreateBehaviorListEv : BeanObjectBase, ISarReportSttCreate
    {
        List<SAR_REPORT_STT> entities;

        internal SarReportSttCreateBehaviorListEv(CommonParam param, List<SAR_REPORT_STT> datas)
            : base(param)
        {
            entities = datas;
        }

        bool ISarReportSttCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SarReportSttDAO.CreateList(entities);
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
                result = result && SarReportSttCheckVerifyValidData.Verify(param, entities);
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
