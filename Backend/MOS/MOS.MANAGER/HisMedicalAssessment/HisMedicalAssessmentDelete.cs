using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMedicalAssessment
{
    partial class HisMedicalAssessmentDelete : BusinessBase
    {
        internal HisMedicalAssessmentDelete()
            : base()
        {

        }

        internal HisMedicalAssessmentDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_MEDICAL_ASSESSMENT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMedicalAssessmentCheck checker = new HisMedicalAssessmentCheck(param);
                valid = valid && IsNotNull(data);
                HIS_MEDICAL_ASSESSMENT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisMedicalAssessmentDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_MEDICAL_ASSESSMENT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMedicalAssessmentCheck checker = new HisMedicalAssessmentCheck(param);
                List<HIS_MEDICAL_ASSESSMENT> listRaw = new List<HIS_MEDICAL_ASSESSMENT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisMedicalAssessmentDAO.DeleteList(listData);
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
