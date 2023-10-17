using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisUnlimitReason
{
    partial class HisUnlimitReasonGet : EntityBase
    {
        public Dictionary<string, HIS_UNLIMIT_REASON> GetDicByCode(HisUnlimitReasonSO search, CommonParam param)
        {
            Dictionary<string, HIS_UNLIMIT_REASON> dic = new Dictionary<string, HIS_UNLIMIT_REASON>();
            try
            {
                List<HIS_UNLIMIT_REASON> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.UNLIMIT_REASON_CODE))
                        {
                            dic.Add(item.UNLIMIT_REASON_CODE, item);
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
