using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAppointmentServ
{
    partial class HisAppointmentServDelete : BusinessBase
    {
        internal HisAppointmentServDelete()
            : base()
        {

        }

        internal HisAppointmentServDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_APPOINTMENT_SERV data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAppointmentServCheck checker = new HisAppointmentServCheck(param);
                valid = valid && IsNotNull(data);
                HIS_APPOINTMENT_SERV raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisAppointmentServDAO.Delete(data);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal bool DeleteList(List<HIS_APPOINTMENT_SERV> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAppointmentServCheck checker = new HisAppointmentServCheck(param);
                List<HIS_APPOINTMENT_SERV> listRaw = new List<HIS_APPOINTMENT_SERV>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisAppointmentServDAO.DeleteList(listData);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
