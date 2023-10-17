using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMedicineTypeTut
{
    partial class HisMedicineTypeTutDelete : BusinessBase
    {
        internal HisMedicineTypeTutDelete()
            : base()
        {

        }

        internal HisMedicineTypeTutDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_MEDICINE_TYPE_TUT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMedicineTypeTutCheck checker = new HisMedicineTypeTutCheck(param);
                valid = valid && IsNotNull(data);
                HIS_MEDICINE_TYPE_TUT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisMedicineTypeTutDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_MEDICINE_TYPE_TUT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMedicineTypeTutCheck checker = new HisMedicineTypeTutCheck(param);
                List<HIS_MEDICINE_TYPE_TUT> listRaw = new List<HIS_MEDICINE_TYPE_TUT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisMedicineTypeTutDAO.DeleteList(listData);
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
