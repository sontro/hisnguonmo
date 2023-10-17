using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTreatmentEndTypeExt
{
    partial class HisTreatmentEndTypeExtDelete : BusinessBase
    {
        internal HisTreatmentEndTypeExtDelete()
            : base()
        {

        }

        internal HisTreatmentEndTypeExtDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_TREATMENT_END_TYPE_EXT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTreatmentEndTypeExtCheck checker = new HisTreatmentEndTypeExtCheck(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT_END_TYPE_EXT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisTreatmentEndTypeExtDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_TREATMENT_END_TYPE_EXT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTreatmentEndTypeExtCheck checker = new HisTreatmentEndTypeExtCheck(param);
                List<HIS_TREATMENT_END_TYPE_EXT> listRaw = new List<HIS_TREATMENT_END_TYPE_EXT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisTreatmentEndTypeExtDAO.DeleteList(listData);
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
