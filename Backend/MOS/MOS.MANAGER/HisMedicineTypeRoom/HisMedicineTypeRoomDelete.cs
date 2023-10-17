using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMedicineTypeRoom
{
    partial class HisMedicineTypeRoomDelete : BusinessBase
    {
        internal HisMedicineTypeRoomDelete()
            : base()
        {

        }

        internal HisMedicineTypeRoomDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_MEDICINE_TYPE_ROOM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMedicineTypeRoomCheck checker = new HisMedicineTypeRoomCheck(param);
                valid = valid && IsNotNull(data);
                HIS_MEDICINE_TYPE_ROOM raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisMedicineTypeRoomDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_MEDICINE_TYPE_ROOM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMedicineTypeRoomCheck checker = new HisMedicineTypeRoomCheck(param);
                List<HIS_MEDICINE_TYPE_ROOM> listRaw = new List<HIS_MEDICINE_TYPE_ROOM>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisMedicineTypeRoomDAO.DeleteList(listData);
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
