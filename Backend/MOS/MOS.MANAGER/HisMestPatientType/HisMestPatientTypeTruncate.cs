using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestPatientType
{
    class HisMestPatientTypeTruncate : BusinessBase
    {
        internal HisMestPatientTypeTruncate()
            : base()
        {

        }

        internal HisMestPatientTypeTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_MEST_PATIENT_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMestPatientTypeCheck checker = new HisMestPatientTypeCheck(param);
                valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                if (valid)
                {
                    result = DAOWorker.HisMestPatientTypeDAO.Truncate(data);
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

        internal bool TruncateList(List<HIS_MEST_PATIENT_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMestPatientTypeCheck checker = new HisMestPatientTypeCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisMestPatientTypeDAO.TruncateList(listData);
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

        internal bool TruncateList(List<long> Ids)
        {
            bool result = false;
            try
            {
                if (Ids != null)
                {
                    List<HIS_MEST_PATIENT_TYPE> mestPatientTypes = new HisMestPatientTypeGet().GetByIds(Ids);
                    result = this.TruncateList(mestPatientTypes);
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

        internal bool TruncateByMediStockId(long mediStockId)
        {
            bool result = false;
            try
            {
                List<HIS_MEST_PATIENT_TYPE> mestPatientTypes = new HisMestPatientTypeGet().GetByMediStockId(mediStockId);
                if (IsNotNullOrEmpty(mestPatientTypes))
                {
                    result = this.TruncateList(mestPatientTypes);
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

        internal bool TruncateByPatientTypeId(long patientTypeId)
        {
            bool result = false;
            try
            {
                List<HIS_MEST_PATIENT_TYPE> mestPatientTypes = new HisMestPatientTypeGet().GetByPatientTypeId(patientTypeId);
                if (IsNotNullOrEmpty(mestPatientTypes))
                {
                    result = this.TruncateList(mestPatientTypes);
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
