using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRoom;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSampleRoom
{
    class HisSampleRoomDelete : BusinessBase
    {
        internal HisSampleRoomDelete()
            : base()
        {

        }

        internal HisSampleRoomDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_EXECUTE_ROOM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSampleRoomCheck checker = new HisSampleRoomCheck(param);
                valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                if (valid)
                {
                    if (new HisRoomDelete(param).Delete(data.ROOM_ID))
                    {
                        result = DAOWorker.HisSampleRoomDAO.Delete(data);
                    }
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

        internal bool DeleteList(List<HIS_EXECUTE_ROOM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSampleRoomCheck checker = new HisSampleRoomCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisSampleRoomDAO.DeleteList(listData);
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
