using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisPatientTypeSub
{
    partial class HisPatientTypeSubDelete : BusinessBase
    {
        internal HisPatientTypeSubDelete()
            : base()
        {

        }

        internal HisPatientTypeSubDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_PATIENT_TYPE_SUB data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPatientTypeSubCheck checker = new HisPatientTypeSubCheck(param);
                valid = valid && IsNotNull(data);
                HIS_PATIENT_TYPE_SUB raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisPatientTypeSubDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_PATIENT_TYPE_SUB> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPatientTypeSubCheck checker = new HisPatientTypeSubCheck(param);
                List<HIS_PATIENT_TYPE_SUB> listRaw = new List<HIS_PATIENT_TYPE_SUB>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisPatientTypeSubDAO.DeleteList(listData);
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
