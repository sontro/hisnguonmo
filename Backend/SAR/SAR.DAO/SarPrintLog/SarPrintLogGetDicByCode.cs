using SAR.DAO.Base;
using SAR.DAO.StagingObject;
using SAR.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAR.DAO.SarPrintLog
{
    partial class SarPrintLogGet : EntityBase
    {
        public Dictionary<string, SAR_PRINT_LOG> GetDicByCode(SarPrintLogSO search, CommonParam param)
        {
            Dictionary<string, SAR_PRINT_LOG> dic = new Dictionary<string, SAR_PRINT_LOG>();
            try
            {
                List<SAR_PRINT_LOG> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.PRINT_LOG_CODE))
                        {
                            dic.Add(item.PRINT_LOG_CODE, item);
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
