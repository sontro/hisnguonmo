using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarUserReportType.CreateList
{
    class SarUserReportTypeCreateListBehaviorEv : BeanObjectBase, ISarUserReportTypeCreateList
    {
        List<SAR_USER_REPORT_TYPE> entity;

        internal SarUserReportTypeCreateListBehaviorEv(CommonParam param, List<SAR_USER_REPORT_TYPE> data)
            : base(param)
        {
            entity = data;
        }

        bool ISarUserReportTypeCreateList.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SarUserReportTypeDAO.CreateList(entity);
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
