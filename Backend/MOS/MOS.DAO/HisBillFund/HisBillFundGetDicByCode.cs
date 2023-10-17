using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBillFund
{
    partial class HisBillFundGet : EntityBase
    {
        public Dictionary<string, HIS_BILL_FUND> GetDicByCode(HisBillFundSO search, CommonParam param)
        {
            Dictionary<string, HIS_BILL_FUND> dic = new Dictionary<string, HIS_BILL_FUND>();
            try
            {
                List<HIS_BILL_FUND> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.BILL_FUND_CODE))
                        {
                            dic.Add(item.BILL_FUND_CODE, item);
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
