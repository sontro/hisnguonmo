using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMestPeriodMate
{
    partial class HisMestPeriodMateGet : EntityBase
    {
        public Dictionary<string, HIS_MEST_PERIOD_MATE> GetDicByCode(HisMestPeriodMateSO search, CommonParam param)
        {
            Dictionary<string, HIS_MEST_PERIOD_MATE> dic = new Dictionary<string, HIS_MEST_PERIOD_MATE>();
            try
            {
                List<HIS_MEST_PERIOD_MATE> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.MEST_PERIOD_MATE_CODE))
                        {
                            dic.Add(item.MEST_PERIOD_MATE_CODE, item);
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
