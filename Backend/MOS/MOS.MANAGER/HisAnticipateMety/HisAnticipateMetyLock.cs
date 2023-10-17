using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;

namespace MOS.MANAGER.HisAnticipateMety
{
    partial class HisAnticipateMetyLock : BusinessBase
    {
        internal HisAnticipateMetyLock()
            : base()
        {

        }

        internal HisAnticipateMetyLock(CommonParam paramLock)
            : base(paramLock)
        {

        }

        internal bool ChangeLock(HIS_ANTICIPATE_METY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_ANTICIPATE_METY raw = null;
                valid = valid && new HisAnticipateMetyCheck().VerifyId(data.ID, ref raw);
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
                    result = DAOWorker.HisAnticipateMetyDAO.Update(raw);
                    if (result) data.IS_ACTIVE = raw.IS_ACTIVE;
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
