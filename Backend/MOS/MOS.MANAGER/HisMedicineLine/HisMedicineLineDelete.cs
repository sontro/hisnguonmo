using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMedicineLine
{
    partial class HisMedicineLineDelete : BusinessBase
    {
        internal HisMedicineLineDelete()
            : base()
        {

        }

        internal HisMedicineLineDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_MEDICINE_LINE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMedicineLineCheck checker = new HisMedicineLineCheck(param);
                valid = valid && IsNotNull(data);
                HIS_MEDICINE_LINE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisMedicineLineDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_MEDICINE_LINE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMedicineLineCheck checker = new HisMedicineLineCheck(param);
                List<HIS_MEDICINE_LINE> listRaw = new List<HIS_MEDICINE_LINE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisMedicineLineDAO.DeleteList(listData);
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
