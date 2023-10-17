using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRoom;
using System;

namespace MOS.MANAGER.HisMediStock
{
    class HisMediStockLock : BusinessBase
    {
        internal HisMediStockLock()
            : base()
        {

        }

        internal HisMediStockLock(CommonParam paramLock)
            : base(paramLock)
        {

        }

        internal bool ChangeLock(HIS_MEDI_STOCK data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_MEDI_STOCK raw = null;
                valid = valid && new HisMediStockCheck().VerifyId(data.ID, ref raw);
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
                    result = DAOWorker.HisMediStockDAO.Update(raw);
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

        internal bool UnLock(long id)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsGreaterThanZero(id);
                if (valid)
                {
                    HIS_MEDI_STOCK data = new HisMediStockGet().GetById(id);
                    if (data != null)
                    {
                        if (data.IS_ACTIVE != IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE)
                        {
                            data.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE;
                            result = DAOWorker.HisMediStockDAO.Update(data);
                            if (result)
                            {
                                new HisRoomLock().ChangeLock(data.ROOM_ID, data.IS_ACTIVE);
                            }
                        }
                        else
                        {
                            result = true;
                        }
                    }
                    else
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
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

        internal bool Lock(long id)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsGreaterThanZero(id);
                if (valid)
                {
                    HIS_MEDI_STOCK data = new HisMediStockGet().GetById(id);
                    if (data != null)
                    {
                        if (data.IS_ACTIVE.HasValue && data.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE)
                        {
                            data.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__FALSE;
                        }
                        else
                        {
                            data.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE;
                        }
                        result = DAOWorker.HisMediStockDAO.Update(data);
                        if (result)
                        {
                            new HisRoomLock().ChangeLock(data.ROOM_ID, data.IS_ACTIVE);
                        }
                    }
                    else
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
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
    }
}
