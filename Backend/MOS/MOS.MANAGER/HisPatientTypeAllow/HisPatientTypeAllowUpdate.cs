using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisPatientTypeAllow
{
    class HisPatientTypeAllowUpdate : BusinessBase
    {
        internal HisPatientTypeAllowUpdate()
            : base()
        {

        }

        internal HisPatientTypeAllowUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_PATIENT_TYPE_ALLOW data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPatientTypeAllowCheck checker = new HisPatientTypeAllowCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && IsGreaterThanZero(data.ID);
                HIS_PATIENT_TYPE_ALLOW raw = null;
                valid = valid && checker.IsGreaterThanZero(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisPatientTypeAllowDAO.Update(data);
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

        internal bool UpdateList(List<HIS_PATIENT_TYPE_ALLOW> listData)
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
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    result = DAOWorker.HisPatientTypeAllowDAO.UpdateList(listData);
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
