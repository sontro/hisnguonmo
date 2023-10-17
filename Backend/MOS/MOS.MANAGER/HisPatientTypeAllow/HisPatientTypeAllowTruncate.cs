using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisPatientTypeAllow
{
    class HisPatientTypeAllowTruncate : BusinessBase
    {
        internal HisPatientTypeAllowTruncate()
            : base()
        {

        }

        internal HisPatientTypeAllowTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_PATIENT_TYPE_ALLOW data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPatientTypeAllowCheck checker = new HisPatientTypeAllowCheck(param);
                valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                HIS_PATIENT_TYPE_ALLOW raw = null;
                valid = valid && checker.IsGreaterThanZero(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisPatientTypeAllowDAO.Truncate(data);
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

        internal bool TruncateList(List<HIS_PATIENT_TYPE_ALLOW> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPatientTypeAllowCheck checker = new HisPatientTypeAllowCheck(param);
                List<HIS_PATIENT_TYPE_ALLOW> listRaw = new List<HIS_PATIENT_TYPE_ALLOW>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisPatientTypeAllowDAO.TruncateList(listData);
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

        internal bool TruncateByPatientTypeIdOrPatientTypeAllowId(long patientTypeId)
        {
            bool result = false;
            try
            {
                List<HIS_PATIENT_TYPE_ALLOW> patientTypeAllows = new HisPatientTypeAllowGet().GetByPatientTypeIdOrPatientTypeAllowId(patientTypeId);
                if (IsNotNullOrEmpty(patientTypeAllows))
                {
                    result = this.TruncateList(patientTypeAllows);
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
