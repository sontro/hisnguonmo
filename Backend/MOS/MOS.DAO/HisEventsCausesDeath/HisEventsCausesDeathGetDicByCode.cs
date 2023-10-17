using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisEventsCausesDeath
{
    partial class HisEventsCausesDeathGet : EntityBase
    {
        public Dictionary<string, HIS_EVENTS_CAUSES_DEATH> GetDicByCode(HisEventsCausesDeathSO search, CommonParam param)
        {
            Dictionary<string, HIS_EVENTS_CAUSES_DEATH> dic = new Dictionary<string, HIS_EVENTS_CAUSES_DEATH>();
            try
            {
                List<HIS_EVENTS_CAUSES_DEATH> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.EVENTS_CAUSES_DEATH_CODE))
                        {
                            dic.Add(item.EVENTS_CAUSES_DEATH_CODE, item);
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
