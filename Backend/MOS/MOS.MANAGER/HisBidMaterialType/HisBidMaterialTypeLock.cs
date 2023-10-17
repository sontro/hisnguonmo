using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;

namespace MOS.MANAGER.HisBidMaterialType
{
    partial class HisBidMaterialTypeLock : BusinessBase
    {
        internal HisBidMaterialTypeLock()
            : base()
        {

        }

        internal HisBidMaterialTypeLock(CommonParam paramLock)
            : base(paramLock)
        {

        }

        internal bool ChangeLock(HIS_BID_MATERIAL_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_BID_MATERIAL_TYPE raw = null;
                valid = valid && new HisBidMaterialTypeCheck().VerifyId(data.ID, ref raw);
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
                    result = DAOWorker.HisBidMaterialTypeDAO.Update(raw);
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
