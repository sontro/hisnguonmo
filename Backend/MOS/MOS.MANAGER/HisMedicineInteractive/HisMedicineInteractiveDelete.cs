using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMedicineInteractive
{
    partial class HisMedicineInteractiveDelete : BusinessBase
    {
        internal HisMedicineInteractiveDelete()
            : base()
        {

        }

        internal HisMedicineInteractiveDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_MEDICINE_INTERACTIVE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMedicineInteractiveCheck checker = new HisMedicineInteractiveCheck(param);
                valid = valid && IsNotNull(data);
                HIS_MEDICINE_INTERACTIVE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisMedicineInteractiveDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_MEDICINE_INTERACTIVE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMedicineInteractiveCheck checker = new HisMedicineInteractiveCheck(param);
                List<HIS_MEDICINE_INTERACTIVE> listRaw = new List<HIS_MEDICINE_INTERACTIVE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisMedicineInteractiveDAO.DeleteList(listData);
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
