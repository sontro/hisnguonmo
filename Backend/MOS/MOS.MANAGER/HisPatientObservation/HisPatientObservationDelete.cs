using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisPatientObservation
{
    partial class HisPatientObservationDelete : BusinessBase
    {
        internal HisPatientObservationDelete()
            : base()
        {

        }

        internal HisPatientObservationDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_PATIENT_OBSERVATION data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPatientObservationCheck checker = new HisPatientObservationCheck(param);
                valid = valid && IsNotNull(data);
                HIS_PATIENT_OBSERVATION raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisPatientObservationDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_PATIENT_OBSERVATION> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPatientObservationCheck checker = new HisPatientObservationCheck(param);
                List<HIS_PATIENT_OBSERVATION> listRaw = new List<HIS_PATIENT_OBSERVATION>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisPatientObservationDAO.DeleteList(listData);
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
