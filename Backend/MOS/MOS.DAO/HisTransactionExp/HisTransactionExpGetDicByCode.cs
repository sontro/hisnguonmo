using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTransactionExp
{
    partial class HisTransactionExpGet : EntityBase
    {
        public Dictionary<string, HIS_TRANSACTION_EXP> GetDicByCode(HisTransactionExpSO search, CommonParam param)
        {
            Dictionary<string, HIS_TRANSACTION_EXP> dic = new Dictionary<string, HIS_TRANSACTION_EXP>();
            try
            {
                List<HIS_TRANSACTION_EXP> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.TRANSACTION_EXP_CODE))
                        {
                            dic.Add(item.TRANSACTION_EXP_CODE, item);
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
