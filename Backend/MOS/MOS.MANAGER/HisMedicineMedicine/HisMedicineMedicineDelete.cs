using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMedicineMedicine
{
    partial class HisMedicineMedicineDelete : BusinessBase
    {
        internal HisMedicineMedicineDelete()
            : base()
        {

        }

        internal HisMedicineMedicineDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_MEDICINE_MEDICINE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMedicineMedicineCheck checker = new HisMedicineMedicineCheck(param);
                valid = valid && IsNotNull(data);
                HIS_MEDICINE_MEDICINE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisMedicineMedicineDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_MEDICINE_MEDICINE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMedicineMedicineCheck checker = new HisMedicineMedicineCheck(param);
                List<HIS_MEDICINE_MEDICINE> listRaw = new List<HIS_MEDICINE_MEDICINE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisMedicineMedicineDAO.DeleteList(listData);
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
