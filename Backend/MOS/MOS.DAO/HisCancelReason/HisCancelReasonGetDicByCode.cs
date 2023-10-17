using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisCancelReason
{
    partial class HisCancelReasonGet : EntityBase
    {
        public Dictionary<string, HIS_CANCEL_REASON> GetDicByCode(HisCancelReasonSO search, CommonParam param)
        {
            Dictionary<string, HIS_CANCEL_REASON> dic = new Dictionary<string, HIS_CANCEL_REASON>();
            try
            {
                List<HIS_CANCEL_REASON> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.CANCEL_REASON_CODE))
                        {
                            dic.Add(item.CANCEL_REASON_CODE, item);
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
