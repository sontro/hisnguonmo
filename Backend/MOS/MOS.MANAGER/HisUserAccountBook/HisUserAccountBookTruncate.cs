using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRestRetrType;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisUserAccountBook
{
    partial class HisUserAccountBookTruncate : BusinessBase
    {
        internal HisUserAccountBookTruncate()
            : base()
        {

        }

        internal HisUserAccountBookTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(long id)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisUserAccountBookCheck checker = new HisUserAccountBookCheck(param);
                HIS_USER_ACCOUNT_BOOK raw = null;
                valid = valid && checker.VerifyId(id, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(id);
                if (valid)
                {
                    result = DAOWorker.HisUserAccountBookDAO.Truncate(raw);
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

        internal bool TruncateByAccountBookId(long accountBookId)
        {
            try
            {
                List<HIS_USER_ACCOUNT_BOOK> userAccountBooks = new HisUserAccountBookGet().GetByAccountBookId(accountBookId);
                if (!IsNotNullOrEmpty(userAccountBooks))
                {
                    return true;
                }
                return DAOWorker.HisUserAccountBookDAO.TruncateList(userAccountBooks); 
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return false;
        }

        internal bool TruncateList(List<HIS_USER_ACCOUNT_BOOK> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisUserAccountBookCheck checker = new HisUserAccountBookCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
					valid = valid && checker.CheckConstraint(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisUserAccountBookDAO.TruncateList(listData);
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
                List<HIS_USER_ACCOUNT_BOOK> listRaw = new List<HIS_USER_ACCOUNT_BOOK>();
                valid = IsNotNullOrEmpty(ids);
                HisUserAccountBookCheck checker = new HisUserAccountBookCheck(param);
                valid = valid && checker.VerifyIds(ids, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listRaw)
                {
                    valid = valid && checker.CheckConstraint(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisUserAccountBookDAO.TruncateList(listRaw);
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
