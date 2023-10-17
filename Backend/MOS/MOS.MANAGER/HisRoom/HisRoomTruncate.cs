using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisMestRoom;
using MOS.MANAGER.HisServiceRoom;
using MOS.MANAGER.HisUserRoom;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRoom
{
    class HisRoomTruncate : BusinessBase
    {
        internal HisRoomTruncate()
            : base()
        {

        }

        internal HisRoomTruncate(Inventec.Core.CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_ROOM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRoomCheck checker = new HisRoomCheck(param);
                valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                if (valid)
                {
                    result = DAOWorker.HisRoomDAO.Truncate(data);
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

        internal bool TruncateList(List<HIS_ROOM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRoomCheck checker = new HisRoomCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisRoomDAO.TruncateList(listData);
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

        /// <summary>
        /// Ham nay se duoc goi tu cac ham truncate mediStock, data_store, execute_room, ... chu ko goi tu client.
        /// Viec check constraint se check tu cac ham do
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal bool Truncate(long id)
        {
            bool result = false;
            try
            {
                HisRoomCheck checker = new HisRoomCheck(param);
                HIS_ROOM data = null;
                bool valid = checker.VerifyId(id, ref data);
                valid = valid && checker.IsUnLock(data);
                if (valid)
                {
                    //truncate cac danh muc lien quan
                    new HisServiceRoomTruncate(param).TruncateByRoomId(id);
                    new HisUserRoomTruncate(param).TruncateByRoomId(id);
                    new HisMestRoomTruncate(param).TruncateByRoomId(id);
                    result = DAOWorker.HisRoomDAO.Truncate(data);
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
