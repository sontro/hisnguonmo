using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRationSchedule
{
    partial class HisRationScheduleGet : EntityBase
    {
        public Dictionary<string, HIS_RATION_SCHEDULE> GetDicByCode(HisRationScheduleSO search, CommonParam param)
        {
            Dictionary<string, HIS_RATION_SCHEDULE> dic = new Dictionary<string, HIS_RATION_SCHEDULE>();
            try
            {
                List<HIS_RATION_SCHEDULE> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.RATION_SCHEDULE_CODE))
                        {
                            dic.Add(item.RATION_SCHEDULE_CODE, item);
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
