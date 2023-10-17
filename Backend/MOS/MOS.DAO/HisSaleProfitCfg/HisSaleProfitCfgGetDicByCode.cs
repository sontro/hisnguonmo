using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSaleProfitCfg
{
    partial class HisSaleProfitCfgGet : EntityBase
    {
        public Dictionary<string, HIS_SALE_PROFIT_CFG> GetDicByCode(HisSaleProfitCfgSO search, CommonParam param)
        {
            Dictionary<string, HIS_SALE_PROFIT_CFG> dic = new Dictionary<string, HIS_SALE_PROFIT_CFG>();
            try
            {
                List<HIS_SALE_PROFIT_CFG> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.SALE_PROFIT_CFG_CODE))
                        {
                            dic.Add(item.SALE_PROFIT_CFG_CODE, item);
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
