using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTreatmentEndType
{
    partial class HisTreatmentEndTypeTruncate : BusinessBase
    {
        internal HisTreatmentEndTypeTruncate()
            : base()
        {

        }

        internal HisTreatmentEndTypeTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_TREATMENT_END_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTreatmentEndTypeCheck checker = new HisTreatmentEndTypeCheck(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT_END_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckAllowUpdateOrDelete(data);
                valid = valid && checker.CheckConstraint(data.ID);
                if (valid)
                {
                    result = DAOWorker.HisTreatmentEndTypeDAO.Truncate(data);
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

        internal bool TruncateList(List<HIS_TREATMENT_END_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTreatmentEndTypeCheck checker = new HisTreatmentEndTypeCheck(param);
                List<HIS_TREATMENT_END_TYPE> listRaw = new List<HIS_TREATMENT_END_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                valid = valid && checker.CheckAllowUpdateOrDelete(listData);
                valid = valid && checker.CheckConstraint(listId);
                if (valid)
                {
                    result = DAOWorker.HisTreatmentEndTypeDAO.TruncateList(listData);
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
