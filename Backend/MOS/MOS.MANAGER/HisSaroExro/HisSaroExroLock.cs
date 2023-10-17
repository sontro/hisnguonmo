using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;

namespace MOS.MANAGER.HisSaroExro
{
    partial class HisSaroExroLock : BusinessBase
    {
        internal HisSaroExroLock()
            : base()
        {

        }

        internal HisSaroExroLock(CommonParam paramLock)
            : base(paramLock)
        {

        }

        internal bool ChangeLock(HIS_SARO_EXRO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_SARO_EXRO raw = null;
                valid = valid && new HisSaroExroCheck().VerifyId(data.ID, ref raw);
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
                    result = DAOWorker.HisSaroExroDAO.Update(raw);
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
