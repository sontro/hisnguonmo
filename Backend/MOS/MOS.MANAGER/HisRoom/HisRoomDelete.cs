using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRoom
{
    class HisRoomDelete : BusinessBase
    {
        internal HisRoomDelete()
            : base()
        {

        }

        internal HisRoomDelete(Inventec.Core.CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_ROOM data)
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
                    result = DAOWorker.HisRoomDAO.Delete(data);
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

        internal bool Delete(long id)
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
                    result = DAOWorker.HisRoomDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_ROOM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisRoomCheck checker = new HisRoomCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisRoomDAO.DeleteList(listData);
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
