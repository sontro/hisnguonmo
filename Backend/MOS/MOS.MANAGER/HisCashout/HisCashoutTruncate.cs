using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRestRetrType;
using MOS.MANAGER.HisTransaction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisCashout
{
    partial class HisCashoutTruncate : BusinessBase
    {
        private HisTransactionUpdate hisTransactionUpdate;

        internal HisCashoutTruncate()
            : base()
        {
            this.hisTransactionUpdate = new HisTransactionUpdate(param);
        }

        internal HisCashoutTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {
            this.hisTransactionUpdate = new HisTransactionUpdate(param);
        }

        internal bool Truncate(long id)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisCashoutCheck checker = new HisCashoutCheck(param);
                HIS_CASHOUT raw = null;
                valid = valid && checker.VerifyId(id, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(id);
                if (valid)
                {
                    //Lay ra cac giao dich da duoc nop quy va gio ko co trong d/s gui len
                    List<HIS_TRANSACTION> toUpdateTransactions = new HisTransactionGet().GetByCashoutId(id);
                    if (IsNotNullOrEmpty(toUpdateTransactions))
                    {
                        Mapper.CreateMap<HIS_TRANSACTION, HIS_TRANSACTION>();
                        List<HIS_TRANSACTION> beforeUpdateTransactions = Mapper.Map<List<HIS_TRANSACTION>>(toUpdateTransactions);

                        toUpdateTransactions.ForEach(o => o.CASHOUT_ID = null);
                        if (!this.hisTransactionUpdate.UpdateList(toUpdateTransactions, beforeUpdateTransactions))
                        {
                            throw new Exception("Rollback du lieu");
                        }
                    }

                    if (!DAOWorker.HisCashoutDAO.Truncate(raw))
                    {
                        throw new Exception("Rollback du lieu");
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                this.hisTransactionUpdate.RollbackData();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal bool TruncateList(List<HIS_CASHOUT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisCashoutCheck checker = new HisCashoutCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
					valid = valid && checker.CheckConstraint(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisCashoutDAO.TruncateList(listData);
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
