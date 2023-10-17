using SAR.DAO.Base;
using SAR.DAO.StagingObject;
using SAR.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAR.DAO.SarReportTypeGroup
{
    partial class SarReportTypeGroupGet : EntityBase
    {
        public Dictionary<string, SAR_REPORT_TYPE_GROUP> GetDicByCode(SarReportTypeGroupSO search, CommonParam param)
        {
            Dictionary<string, SAR_REPORT_TYPE_GROUP> dic = new Dictionary<string, SAR_REPORT_TYPE_GROUP>();
            try
            {
                List<SAR_REPORT_TYPE_GROUP> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.REPORT_TYPE_GROUP_CODE))
                        {
                            dic.Add(item.REPORT_TYPE_GROUP_CODE, item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logging(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => search), search) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => param), param), LogType.Error);
                LogSystem.Error(ex);
                dic.Clear();
            }
            return dic;
        }
    }
}
