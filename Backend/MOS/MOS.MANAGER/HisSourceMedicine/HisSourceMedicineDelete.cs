using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSourceMedicine
{
    partial class HisSourceMedicineDelete : BusinessBase
    {
        internal HisSourceMedicineDelete()
            : base()
        {

        }

        internal HisSourceMedicineDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_SOURCE_MEDICINE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSourceMedicineCheck checker = new HisSourceMedicineCheck(param);
                valid = valid && IsNotNull(data);
                HIS_SOURCE_MEDICINE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisSourceMedicineDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_SOURCE_MEDICINE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSourceMedicineCheck checker = new HisSourceMedicineCheck(param);
                List<HIS_SOURCE_MEDICINE> listRaw = new List<HIS_SOURCE_MEDICINE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisSourceMedicineDAO.DeleteList(listData);
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
