using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisUserRoom
{
    class HisUserRoomTruncate : BusinessBase
    {
        internal HisUserRoomTruncate()
            : base()
        {

        }

        internal HisUserRoomTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_USER_ROOM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisUserRoomCheck checker = new HisUserRoomCheck(param);
                valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                if (valid)
                {
                    result = DAOWorker.HisUserRoomDAO.Truncate(data);
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

        internal bool TruncateList(List<long> ids)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(ids);
                HisUserRoomCheck checker = new HisUserRoomCheck(param);
                List<HIS_USER_ROOM> listRaw = new List<HIS_USER_ROOM>();
                valid = valid && checker.VerifyIds(ids, listRaw);
                if (valid)
                {
                    result = this.TruncateList(listRaw);
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

        internal bool TruncateList(List<HIS_USER_ROOM> listRaw)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listRaw);
                HisUserRoomCheck checker = new HisUserRoomCheck(param);
                valid = valid && checker.IsUnLock(listRaw);

                if (valid)
                {
                    result = DAOWorker.HisUserRoomDAO.TruncateList(listRaw);
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

        internal bool TruncateByRoomId(long roomId)
        {
            bool result = false;
            try
            {
                List<HIS_USER_ROOM> userRooms = new HisUserRoomGet().GetByRoomId(roomId);
                if (IsNotNullOrEmpty(userRooms))
                {
                    result = TruncateList(userRooms);
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
