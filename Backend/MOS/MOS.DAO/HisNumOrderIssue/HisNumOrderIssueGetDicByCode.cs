using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisNumOrderIssue
{
    partial class HisNumOrderIssueGet : EntityBase
    {
        public Dictionary<string, HIS_NUM_ORDER_ISSUE> GetDicByCode(HisNumOrderIssueSO search, CommonParam param)
        {
            Dictionary<string, HIS_NUM_ORDER_ISSUE> dic = new Dictionary<string, HIS_NUM_ORDER_ISSUE>();
            try
            {
                List<HIS_NUM_ORDER_ISSUE> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.NUM_ORDER_ISSUE_CODE))
                        {
                            dic.Add(item.NUM_ORDER_ISSUE_CODE, item);
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
