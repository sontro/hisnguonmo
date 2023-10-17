using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBlood
{
    partial class HisBloodLock : BusinessBase
    {
        internal HisBloodLock()
            : base()
        {

        }

        internal HisBloodLock(CommonParam paramLock)
            : base(paramLock)
        {

        }

        internal bool Lock(long id, ref HIS_BLOOD resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsGreaterThanZero(id);
                if (valid)
                {
                    HIS_BLOOD data = new HisBloodGet().GetById(id);
                    if (data != null)
                    {
                        data.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__FALSE;
                        result = DAOWorker.HisBloodDAO.Update(data);
                        resultData = result ? data : null;
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

        internal bool Unlock(long id, ref HIS_BLOOD resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsGreaterThanZero(id);
                if (valid)
                {
                    HIS_BLOOD data = new HisBloodGet().GetById(id);
                    if (data != null)
                    {
                        data.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE;
                        result = DAOWorker.HisBloodDAO.Update(data);
                        resultData = result ? data : null;
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

        internal bool UnlockList(List<long> ids)
        {
            try
            {
                if (IsNotNullOrEmpty(ids))
                {
                    string query = DAOWorker.SqlDAO.AddInClause(ids, "UPDATE HIS_BLOOD SET IS_ACTIVE = 1 WHERE %IN_CLAUSE% ", "ID");
                    if (!DAOWorker.SqlDAO.Execute(query))
                    {
                        LogSystem.Error("lock blood that bai");
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }
            return false;
        }

        internal bool ChangeLock(HIS_BLOOD data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_BLOOD raw = null;
                valid = valid && new HisBloodCheck().VerifyId(data.ID, ref raw);
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
                    result = DAOWorker.HisBloodDAO.Update(raw);
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

        internal bool LockList(List<long> ids)
        {
            try
            {
                if (IsNotNullOrEmpty(ids))
                {
                    string query = DAOWorker.SqlDAO.AddInClause(ids, "UPDATE HIS_BLOOD SET IS_ACTIVE = 0 WHERE %IN_CLAUSE% ", "ID");
                    if (!DAOWorker.SqlDAO.Execute(query))
                    {
                        LogSystem.Error("lock blood that bai");
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }
            return false;
        }
    }
}
