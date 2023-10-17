using Inventec.Common.Logging;
using Inventec.Core;
using SDA.SDO;
using System;
using SAR.EFMODEL.DataModels;
using Inventec.Common.WebApiClient;
using SAR.MANAGER.Core;
using SAR.MANAGER.Base;

namespace MRS.MANAGER.Sar.SarReport
{
    class MrsReportCreate : BeanObjectBase
    {
        internal MrsReportCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal SAR.EFMODEL.DataModels.SAR_REPORT Create(MRS.SDO.CreateReportSDO data)
        {
            SAR.EFMODEL.DataModels.SAR_REPORT result = null;
            try
            {
                Inventec.Core.ApiResultObject<SAR.EFMODEL.DataModels.SAR_REPORT> rs = ApiConsumerStore.MrsConsumer.Post<Inventec.Core.ApiResultObject<SAR.EFMODEL.DataModels.SAR_REPORT>>("/api/MrsReport/CreateByCalendar", param, data);
                if (rs != null)
                {
                    if (rs.Param != null)
                    {
                        param.Messages.AddRange(rs.Param.Messages);
                        param.BugCodes.AddRange(rs.Param.BugCodes);
                    }
                    if (rs.Success) result = (rs.Data);
                }
            }
            catch (ApiException ex)
            {
                Inventec.Common.Logging.LogSystem.Info(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => ex.StatusCode), ex.StatusCode));
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }
    }
}
