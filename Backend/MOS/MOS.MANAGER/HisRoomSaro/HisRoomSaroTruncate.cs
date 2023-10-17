using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRestRetrType;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisRoomSaro
{
    partial class HisRoomSaroTruncate : BusinessBase
    {
        internal HisRoomSaroTruncate()
            : base()
        {

        }

        internal HisRoomSaroTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(long id)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRoomSaroCheck checker = new HisRoomSaroCheck(param);
                HIS_ROOM_SARO raw = null;
                valid = valid && checker.VerifyId(id, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(id);
                if (valid)
                {
                    result = DAOWorker.HisRoomSaroDAO.Truncate(raw);
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
                List<HIS_ROOM_SARO> lisRaw = new List<HIS_ROOM_SARO>();
                valid = IsNotNullOrEmpty(ids);
                HisRoomSaroCheck checker = new HisRoomSaroCheck(param);
                valid = valid && checker.VerifyIds(ids, lisRaw);
                valid = valid && checker.IsUnLock(lisRaw);
                if (valid)
                {
                    result = DAOWorker.HisRoomSaroDAO.TruncateList(lisRaw);
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
