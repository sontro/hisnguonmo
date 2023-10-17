using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBidMedicineType
{
    partial class HisBidMedicineTypeDelete : BusinessBase
    {
        internal HisBidMedicineTypeDelete()
            : base()
        {

        }

        internal HisBidMedicineTypeDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_BID_MEDICINE_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBidMedicineTypeCheck checker = new HisBidMedicineTypeCheck(param);
                valid = valid && IsNotNull(data);
                HIS_BID_MEDICINE_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisBidMedicineTypeDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_BID_MEDICINE_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBidMedicineTypeCheck checker = new HisBidMedicineTypeCheck(param);
                List<HIS_BID_MEDICINE_TYPE> listRaw = new List<HIS_BID_MEDICINE_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisBidMedicineTypeDAO.DeleteList(listData);
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
