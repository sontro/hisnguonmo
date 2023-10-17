using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTransactionType
{
    class HisTransactionTypeCreate : BusinessBase
    {
        internal HisTransactionTypeCreate()
            : base()
        {

        }

        internal HisTransactionTypeCreate(Inventec.Core.CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_TRANSACTION_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTransactionTypeCheck checker = new HisTransactionTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.TRANSACTION_TYPE_CODE, null);
                if (valid)
                {
                    result = DAOWorker.HisTransactionTypeDAO.Create(data);
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

        internal bool CreateList(List<HIS_TRANSACTION_TYPE> listData)
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
                    valid = valid && checker.ExistsCode(data.TRANSACTION_TYPE_CODE, null);
                }
                if (valid)
                {
                    result = DAOWorker.HisTransactionTypeDAO.CreateList(listData);
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
