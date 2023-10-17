using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPtttCalendar
{
    partial class HisPtttCalendarGet : EntityBase
    {
        public Dictionary<string, HIS_PTTT_CALENDAR> GetDicByCode(HisPtttCalendarSO search, CommonParam param)
        {
            Dictionary<string, HIS_PTTT_CALENDAR> dic = new Dictionary<string, HIS_PTTT_CALENDAR>();
            try
            {
                List<HIS_PTTT_CALENDAR> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.PTTT_CALENDAR_CODE))
                        {
                            dic.Add(item.PTTT_CALENDAR_CODE, item);
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
