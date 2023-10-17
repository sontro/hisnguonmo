using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;

namespace MOS.MANAGER.HisVaccinationExam
{
    partial class HisVaccinationExamFinish : BusinessBase
    {

        internal HisVaccinationExamFinish()
            : base()
        {

        }

        internal HisVaccinationExamFinish(CommonParam paramFinish)
            : base(paramFinish)
        {

        }

        internal bool Finish(long id, ref HIS_VACCINATION_EXAM resultData)
        {
            bool result = false;
            try
            {
                HIS_VACCINATION_EXAM raw = null;
                HisVaccinationExamCheck checker = new HisVaccinationExamCheck();

                bool valid = true;
                valid = valid && IsGreaterThanZero(id);
                valid = valid && checker.VerifyId(id, ref raw);
                if (valid)
                {
                    raw.VACCINATION_EXAM_STT_ID = (int)HisVaccinationExamUtil.VaccinationExamStt.KET_THUC;
                    result = DAOWorker.HisVaccinationExamDAO.Update(raw);
                    resultData = result ? raw : null;
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

        internal bool CancelFinish(long id, ref HIS_VACCINATION_EXAM resultData)
        {
            bool result = false;
            try
            {
                HIS_VACCINATION_EXAM raw = null;
                HisVaccinationExamCheck checker = new HisVaccinationExamCheck();

                bool valid = true;
                valid = valid && IsGreaterThanZero(id);
                valid = valid && checker.VerifyId(id, ref raw);
                if (valid)
                {
                    raw.VACCINATION_EXAM_STT_ID = (int)HisVaccinationExamUtil.VaccinationExamStt.DANG_XU_LY;
                    result = DAOWorker.HisVaccinationExamDAO.Update(raw);
                    resultData = result ? raw : null;
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
