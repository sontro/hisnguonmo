using Inventec.Common.Logging;
using Inventec.Core;
using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using System;

namespace SDA.MANAGER.SdaCustomizeButton
{
    partial class SdaCustomizeButtonLock : BusinessBase
    {
        internal SdaCustomizeButtonLock()
            : base()
        {

        }

        internal SdaCustomizeButtonLock(CommonParam paramLock)
            : base(paramLock)
        {

        }
		
		internal bool Lock(long id, ref SDA_CUSTOMIZE_BUTTON resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsGreaterThanZero(id);
                if (valid)
                {
                    SDA_CUSTOMIZE_BUTTON data = new SdaCustomizeButtonGet().GetById(id);
                    if (data != null)
                    {
                        data.IS_ACTIVE = IMSys.DbConfig.SDA_RS.COMMON.IS_ACTIVE__FALSE;
                        result = DAOWorker.SdaCustomizeButtonDAO.Update(data);
                        resultData = result ? data : null;
                    }
                    else
                    {
                        SDA.MANAGER.Base.BugUtil.SetBugCode(param, SDA.LibraryBug.Bug.Enum.Common__KXDDDuLieuCanXuLy);
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
		
		internal bool Unlock(long id, ref SDA_CUSTOMIZE_BUTTON resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsGreaterThanZero(id);
                if (valid)
                {
                    SDA_CUSTOMIZE_BUTTON data = new SdaCustomizeButtonGet().GetById(id);
                    if (data != null)
                    {
                        data.IS_ACTIVE = IMSys.DbConfig.SDA_RS.COMMON.IS_ACTIVE__TRUE;
                        result = DAOWorker.SdaCustomizeButtonDAO.Update(data);
                        resultData = result ? data : null;
                    }
                    else
                    {
                        SDA.MANAGER.Base.BugUtil.SetBugCode(param, SDA.LibraryBug.Bug.Enum.Common__KXDDDuLieuCanXuLy);
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

        internal bool ChangeLock(long id, ref SDA_CUSTOMIZE_BUTTON resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                SDA_CUSTOMIZE_BUTTON raw = null;
                valid = valid && new SdaCustomizeButtonCheck().VerifyId(id, ref raw);
                if (valid && raw != null)
                {
                    if (raw.IS_ACTIVE.HasValue && raw.IS_ACTIVE == IMSys.DbConfig.SDA_RS.COMMON.IS_ACTIVE__TRUE)
                    {
                        raw.IS_ACTIVE = IMSys.DbConfig.SDA_RS.COMMON.IS_ACTIVE__FALSE;
                    }
                    else
                    {
                        raw.IS_ACTIVE = IMSys.DbConfig.SDA_RS.COMMON.IS_ACTIVE__TRUE;
                    }
                    result = DAOWorker.SdaCustomizeButtonDAO.Update(raw);
                    if (result) resultData = raw;
                }
                else
                {
                    BugUtil.SetBugCode(param, SDA.LibraryBug.Bug.Enum.Common__KXDDDuLieuCanXuLy);
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
