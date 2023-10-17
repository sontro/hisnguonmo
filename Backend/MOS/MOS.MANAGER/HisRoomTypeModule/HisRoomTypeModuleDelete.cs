using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisRoomTypeModule
{
    partial class HisRoomTypeModuleDelete : BusinessBase
    {
        internal HisRoomTypeModuleDelete()
            : base()
        {

        }

        internal HisRoomTypeModuleDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_ROOM_TYPE_MODULE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRoomTypeModuleCheck checker = new HisRoomTypeModuleCheck(param);
                valid = valid && IsNotNull(data);
                HIS_ROOM_TYPE_MODULE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisRoomTypeModuleDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_ROOM_TYPE_MODULE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisRoomTypeModuleCheck checker = new HisRoomTypeModuleCheck(param);
                List<HIS_ROOM_TYPE_MODULE> listRaw = new List<HIS_ROOM_TYPE_MODULE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisRoomTypeModuleDAO.DeleteList(listData);
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
