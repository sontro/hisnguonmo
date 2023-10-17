using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisPatientType
{
    partial class HisPatientTypeUpdate : BusinessBase
    {
        internal HisPatientTypeUpdate()
            : base()
        {

        }

        internal HisPatientTypeUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_PATIENT_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPatientTypeCheck checker = new HisPatientTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_PATIENT_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.PATIENT_TYPE_CODE, data.ID);
                if (valid)
                {
                    result = DAOWorker.HisPatientTypeDAO.Update(data);
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

        internal bool UpdateList(List<HIS_PATIENT_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPatientTypeCheck checker = new HisPatientTypeCheck(param);
                List<HIS_PATIENT_TYPE> listRaw = new List<HIS_PATIENT_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.PATIENT_TYPE_CODE, data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisPatientTypeDAO.UpdateList(listData);
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
