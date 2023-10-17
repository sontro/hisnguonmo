using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRoom;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBedRoom
{
    class HisBedRoomTruncate : BusinessBase
    {
        internal HisBedRoomTruncate()
            : base()
        {

        }

        internal HisBedRoomTruncate(Inventec.Core.CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_BED_ROOM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBedRoomCheck checker = new HisBedRoomCheck(param);
                valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                valid = valid && checker.CheckConstraint(data.ID);
                if (valid)
                {
                    if (DAOWorker.HisBedRoomDAO.Truncate(data))
                    {
                        result = new HisRoomTruncate(param).Truncate(data.ROOM_ID);
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

        internal bool TruncateList(List<HIS_BED_ROOM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBedRoomCheck checker = new HisBedRoomCheck(param);

                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                    valid = valid && checker.CheckConstraint(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisBedRoomDAO.TruncateList(listData);
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
