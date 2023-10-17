using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisVaccAppointment
{
    partial class HisVaccAppointmentDelete : BusinessBase
    {
        internal HisVaccAppointmentDelete()
            : base()
        {

        }

        internal HisVaccAppointmentDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_VACC_APPOINTMENT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisVaccAppointmentCheck checker = new HisVaccAppointmentCheck(param);
                valid = valid && IsNotNull(data);
                HIS_VACC_APPOINTMENT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisVaccAppointmentDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_VACC_APPOINTMENT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisVaccAppointmentCheck checker = new HisVaccAppointmentCheck(param);
                List<HIS_VACC_APPOINTMENT> listRaw = new List<HIS_VACC_APPOINTMENT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisVaccAppointmentDAO.DeleteList(listData);
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
