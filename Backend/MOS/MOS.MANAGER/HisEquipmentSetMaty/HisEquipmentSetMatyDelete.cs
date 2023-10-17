using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisEquipmentSetMaty
{
    partial class HisEquipmentSetMatyDelete : BusinessBase
    {
        internal HisEquipmentSetMatyDelete()
            : base()
        {

        }

        internal HisEquipmentSetMatyDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_EQUIPMENT_SET_MATY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEquipmentSetMatyCheck checker = new HisEquipmentSetMatyCheck(param);
                valid = valid && IsNotNull(data);
                HIS_EQUIPMENT_SET_MATY raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisEquipmentSetMatyDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_EQUIPMENT_SET_MATY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisEquipmentSetMatyCheck checker = new HisEquipmentSetMatyCheck(param);
                List<HIS_EQUIPMENT_SET_MATY> listRaw = new List<HIS_EQUIPMENT_SET_MATY>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisEquipmentSetMatyDAO.DeleteList(listData);
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
