using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestRoom
{
    class HisMestRoomTruncate : BusinessBase
    {
        internal HisMestRoomTruncate()
            : base()
        {

        }

        internal HisMestRoomTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_MEST_ROOM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMestRoomCheck checker = new HisMestRoomCheck(param);
                valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                if (valid)
                {
                    result = DAOWorker.HisMestRoomDAO.Truncate(data);
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
                HisMestRoomCheck checker = new HisMestRoomCheck(param);
                List<HIS_MEST_ROOM> listRaw = new List<HIS_MEST_ROOM>();
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

        internal bool TruncateList(List<HIS_MEST_ROOM> listRaw)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listRaw);
                HisMestRoomCheck checker = new HisMestRoomCheck(param);
                valid = valid && checker.IsUnLock(listRaw);

                if (valid)
                {
                    result = DAOWorker.HisMestRoomDAO.TruncateList(listRaw);
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
                List<HIS_MEST_ROOM> mestRooms = new HisMestRoomGet().GetByRoomId(roomId);
                if (IsNotNullOrEmpty(mestRooms))
                {
                    result = TruncateList(mestRooms);
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

        internal bool TruncateByMediStockId(long mediStockId)
        {
            bool result = false;
            try
            {
                List<HIS_MEST_ROOM> mestRooms = new HisMestRoomGet().GetByMediStockId(mediStockId);
                if (IsNotNullOrEmpty(mestRooms))
                {
                    result = TruncateList(mestRooms);
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
