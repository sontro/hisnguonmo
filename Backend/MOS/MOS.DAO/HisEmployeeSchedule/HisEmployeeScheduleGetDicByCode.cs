using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisEmployeeSchedule
{
    partial class HisEmployeeScheduleGet : EntityBase
    {
        public Dictionary<string, HIS_EMPLOYEE_SCHEDULE> GetDicByCode(HisEmployeeScheduleSO search, CommonParam param)
        {
            Dictionary<string, HIS_EMPLOYEE_SCHEDULE> dic = new Dictionary<string, HIS_EMPLOYEE_SCHEDULE>();
            try
            {
                List<HIS_EMPLOYEE_SCHEDULE> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.EMPLOYEE_SCHEDULE_CODE))
                        {
                            dic.Add(item.EMPLOYEE_SCHEDULE_CODE, item);
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
