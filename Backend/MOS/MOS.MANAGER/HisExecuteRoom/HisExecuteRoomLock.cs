using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRoom;
using System;

namespace MOS.MANAGER.HisExecuteRoom
{
    class HisExecuteRoomLock : BusinessBase
    {
        internal HisExecuteRoomLock()
            : base()
        {

        }

        internal HisExecuteRoomLock(CommonParam paramLock)
            : base(paramLock)
        {

        }

        internal bool ChangeLock(HIS_EXECUTE_ROOM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_EXECUTE_ROOM raw = null;
                valid = valid && new HisExecuteRoomCheck().VerifyId(data.ID, ref raw);
                if (valid && raw != null)
                {
                    if (raw.IS_ACTIVE.HasValue && raw.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE)
                    {
                        raw.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__FALSE;
                    }
                    else
                    {
                        raw.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE;
                    }
                    result = DAOWorker.HisExecuteRoomDAO.Update(raw);
                    if (result)
                    {
                        new HisRoomLock().ChangeLock(raw.ROOM_ID, raw.IS_ACTIVE);
                        data.IS_ACTIVE = raw.IS_ACTIVE;
                    }
                }
                else
                {
                    BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
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
