using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMixedMedicine
{
    partial class HisMixedMedicineDelete : BusinessBase
    {
        internal HisMixedMedicineDelete()
            : base()
        {

        }

        internal HisMixedMedicineDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_MIXED_MEDICINE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMixedMedicineCheck checker = new HisMixedMedicineCheck(param);
                valid = valid && IsNotNull(data);
                HIS_MIXED_MEDICINE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisMixedMedicineDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_MIXED_MEDICINE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMixedMedicineCheck checker = new HisMixedMedicineCheck(param);
                List<HIS_MIXED_MEDICINE> listRaw = new List<HIS_MIXED_MEDICINE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisMixedMedicineDAO.DeleteList(listData);
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
