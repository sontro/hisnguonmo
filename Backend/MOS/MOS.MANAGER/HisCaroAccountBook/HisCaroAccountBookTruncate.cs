using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRestRetrType;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisCaroAccountBook
{
    partial class HisCaroAccountBookTruncate : BusinessBase
    {
        internal HisCaroAccountBookTruncate()
            : base()
        {

        }

        internal HisCaroAccountBookTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(long id)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisCaroAccountBookCheck checker = new HisCaroAccountBookCheck(param);
                HIS_CARO_ACCOUNT_BOOK raw = null;
                valid = valid && checker.VerifyId(id, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(id);
                if (valid)
                {
                    result = DAOWorker.HisCaroAccountBookDAO.Truncate(raw);
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

        internal bool TruncateList(List<HIS_CARO_ACCOUNT_BOOK> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisCaroAccountBookCheck checker = new HisCaroAccountBookCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
					valid = valid && checker.CheckConstraint(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisCaroAccountBookDAO.TruncateList(listData);
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
                List<HIS_CARO_ACCOUNT_BOOK> listRaw = new List<HIS_CARO_ACCOUNT_BOOK>();
                valid = IsNotNullOrEmpty(ids);
                HisCaroAccountBookCheck checker = new HisCaroAccountBookCheck(param);
                valid = valid && checker.VerifyIds(ids, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listRaw)
                {
                    valid = valid && checker.CheckConstraint(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisCaroAccountBookDAO.TruncateList(listRaw);
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
                List<HIS_CARO_ACCOUNT_BOOK> caroAccountBooks = new HisCaroAccountBookGet().GetByAccountBookId(accountBookId);
                if (!IsNotNullOrEmpty(caroAccountBooks))
                {
                    return true;
                }
                return DAOWorker.HisCaroAccountBookDAO.TruncateList(caroAccountBooks);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return false;
        }
    }
}
