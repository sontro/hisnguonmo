using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSeseDepoRepay
{
    partial class HisSeseDepoRepayGet : EntityBase
    {
        public Dictionary<string, HIS_SESE_DEPO_REPAY> GetDicByCode(HisSeseDepoRepaySO search, CommonParam param)
        {
            Dictionary<string, HIS_SESE_DEPO_REPAY> dic = new Dictionary<string, HIS_SESE_DEPO_REPAY>();
            try
            {
                List<HIS_SESE_DEPO_REPAY> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.SESE_DEPO_REPAY_CODE))
                        {
                            dic.Add(item.SESE_DEPO_REPAY_CODE, item);
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
