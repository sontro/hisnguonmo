using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRoom;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCashierRoom
{
    class HisCashierRoomDelete : BusinessBase
    {
        internal HisCashierRoomDelete()
            : base()
        {

        }

        internal HisCashierRoomDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_CASHIER_ROOM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisCashierRoomCheck checker = new HisCashierRoomCheck(param);
                valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                if (valid)
                {
                    if (new HisRoomDelete(param).Delete(data.ROOM_ID))
                    {
                        result = DAOWorker.HisCashierRoomDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_CASHIER_ROOM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisCashierRoomCheck checker = new HisCashierRoomCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisCashierRoomDAO.DeleteList(listData);
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
