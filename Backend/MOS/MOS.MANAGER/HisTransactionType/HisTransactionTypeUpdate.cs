using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTransactionType
{
    class HisTransactionTypeUpdate : BusinessBase
    {
        internal HisTransactionTypeUpdate()
            : base()
        {

        }

        internal HisTransactionTypeUpdate(Inventec.Core.CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_TRANSACTION_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTransactionTypeCheck checker = new HisTransactionTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data);
                valid = valid && checker.ExistsCode(data.TRANSACTION_TYPE_CODE, data.ID);
                if (valid)
                {
                    result = DAOWorker.HisTransactionTypeDAO.Update(data);
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

        internal bool UpdateList(List<HIS_TRANSACTION_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTransactionTypeCheck checker = new HisTransactionTypeCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data);
                    valid = valid && checker.ExistsCode(data.TRANSACTION_TYPE_CODE, data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisTransactionTypeDAO.UpdateList(listData);
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
