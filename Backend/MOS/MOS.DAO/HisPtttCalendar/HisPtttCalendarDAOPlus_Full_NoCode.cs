using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisPtttCalendar
{
    public partial class HisPtttCalendarDAO : EntityBase
    {
        public List<V_HIS_PTTT_CALENDAR> GetView(HisPtttCalendarSO search, CommonParam param)
        {
            List<V_HIS_PTTT_CALENDAR> result = new List<V_HIS_PTTT_CALENDAR>();
            try
            {
                result = GetWorker.GetView(search, param);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }
            return result;
        }

        public V_HIS_PTTT_CALENDAR GetViewById(long id, HisPtttCalendarSO search)
        {
            V_HIS_PTTT_CALENDAR result = null;

            try
            {
                result = GetWorker.GetViewById(id, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }

            return result;
        }
    }
}
