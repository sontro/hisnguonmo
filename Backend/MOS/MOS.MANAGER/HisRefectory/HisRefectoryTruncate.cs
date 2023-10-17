using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRestRetrType;
using MOS.MANAGER.HisRoom;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisRefectory
{
    partial class HisRefectoryTruncate : BusinessBase
    {
        internal HisRefectoryTruncate()
            : base()
        {

        }

        internal HisRefectoryTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(long id)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRefectoryCheck checker = new HisRefectoryCheck(param);
                HIS_REFECTORY raw = null;
                HisRoomCheck roomChecker = new HisRoomCheck(param);
                valid = valid && IsGreaterThanZero(id);
                valid = valid && checker.VerifyId(id, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && roomChecker.CheckConstraint(raw.ROOM_ID);
                if (valid)
                {
                    if (DAOWorker.HisRefectoryDAO.Truncate(raw))
                    {
                        result = new HisRoomTruncate(param).Truncate(raw.ROOM_ID);
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

        internal bool TruncateList(List<HIS_REFECTORY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisRefectoryCheck checker = new HisRefectoryCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                    valid = valid && checker.CheckConstraint(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisRefectoryDAO.TruncateList(listData);
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
