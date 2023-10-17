using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisPatientProgram
{
    partial class HisPatientProgramDelete : BusinessBase
    {
        internal HisPatientProgramDelete()
            : base()
        {

        }

        internal HisPatientProgramDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_PATIENT_PROGRAM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPatientProgramCheck checker = new HisPatientProgramCheck(param);
                valid = valid && IsNotNull(data);
                HIS_PATIENT_PROGRAM raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisPatientProgramDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_PATIENT_PROGRAM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPatientProgramCheck checker = new HisPatientProgramCheck(param);
                List<HIS_PATIENT_PROGRAM> listRaw = new List<HIS_PATIENT_PROGRAM>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisPatientProgramDAO.DeleteList(listData);
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
