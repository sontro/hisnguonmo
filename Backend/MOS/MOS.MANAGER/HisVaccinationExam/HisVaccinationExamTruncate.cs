using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRestRetrType;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisVaccinationExam
{
    partial class HisVaccinationExamTruncate : BusinessBase
    {
        internal HisVaccinationExamTruncate()
            : base()
        {

        }

        internal HisVaccinationExamTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HisVaccinationExamDeleteSDO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisVaccinationExamCheck checker = new HisVaccinationExamCheck(param);
                HIS_VACCINATION_EXAM raw = null;
                WorkPlaceSDO workPlace = null;
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.VerifyId(data.VaccinationExamId, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && this.HasWorkPlaceInfo(data.RequestRoomId, ref workPlace);
                valid = valid && checker.IsValidUserAccount(raw, workPlace);
                valid = valid && checker.IsValidConclude(raw.CONCLUDE);
                valid = valid && checker.CheckConstraint(raw.ID);
                if (valid)
                {
                    result = DAOWorker.HisVaccinationExamDAO.Truncate(raw);
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

        internal bool TruncateList(List<HIS_VACCINATION_EXAM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisVaccinationExamCheck checker = new HisVaccinationExamCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
					valid = valid && checker.CheckConstraint(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisVaccinationExamDAO.TruncateList(listData);
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
