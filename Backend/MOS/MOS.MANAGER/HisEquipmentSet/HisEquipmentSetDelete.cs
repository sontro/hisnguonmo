using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisEquipmentSet
{
    partial class HisEquipmentSetDelete : BusinessBase
    {
        internal HisEquipmentSetDelete()
            : base()
        {

        }

        internal HisEquipmentSetDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_EQUIPMENT_SET data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEquipmentSetCheck checker = new HisEquipmentSetCheck(param);
                valid = valid && IsNotNull(data);
                HIS_EQUIPMENT_SET raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisEquipmentSetDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_EQUIPMENT_SET> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisEquipmentSetCheck checker = new HisEquipmentSetCheck(param);
                List<HIS_EQUIPMENT_SET> listRaw = new List<HIS_EQUIPMENT_SET>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisEquipmentSetDAO.DeleteList(listData);
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
