using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMedicineGroup
{
    partial class HisMedicineGroupDelete : BusinessBase
    {
        internal HisMedicineGroupDelete()
            : base()
        {

        }

        internal HisMedicineGroupDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_MEDICINE_GROUP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMedicineGroupCheck checker = new HisMedicineGroupCheck(param);
                valid = valid && IsNotNull(data);
                HIS_MEDICINE_GROUP raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisMedicineGroupDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_MEDICINE_GROUP> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMedicineGroupCheck checker = new HisMedicineGroupCheck(param);
                List<HIS_MEDICINE_GROUP> listRaw = new List<HIS_MEDICINE_GROUP>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisMedicineGroupDAO.DeleteList(listData);
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
