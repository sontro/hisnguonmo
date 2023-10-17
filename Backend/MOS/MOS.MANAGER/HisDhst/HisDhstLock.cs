using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;

namespace MOS.MANAGER.HisDhst
{
    class HisDhstLock : BusinessBase
    {
        internal HisDhstLock()
            : base()
        {

        }

        internal HisDhstLock(Inventec.Core.CommonParam paramLock)
            : base(paramLock)
        {

        }

        internal bool ChangeLock(HIS_DHST data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = data != null && IsGreaterThanZero(data.ID);
                if (valid)
                {
                    data = new HisDhstGet().GetById(data.ID);
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
                        result = DAOWorker.HisDhstDAO.Update(data);
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
