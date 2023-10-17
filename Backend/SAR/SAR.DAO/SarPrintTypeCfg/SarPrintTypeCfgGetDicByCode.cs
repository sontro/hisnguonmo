using SAR.DAO.Base;
using SAR.DAO.StagingObject;
using SAR.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAR.DAO.SarPrintTypeCfg
{
    partial class SarPrintTypeCfgGet : EntityBase
    {
        public Dictionary<string, SAR_PRINT_TYPE_CFG> GetDicByCode(SarPrintTypeCfgSO search, CommonParam param)
        {
            Dictionary<string, SAR_PRINT_TYPE_CFG> dic = new Dictionary<string, SAR_PRINT_TYPE_CFG>();
            try
            {
                List<SAR_PRINT_TYPE_CFG> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.PRINT_TYPE_CFG_CODE))
                        {
                            dic.Add(item.PRINT_TYPE_CFG_CODE, item);
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
