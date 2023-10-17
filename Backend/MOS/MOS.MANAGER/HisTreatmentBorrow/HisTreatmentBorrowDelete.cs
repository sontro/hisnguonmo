using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTreatmentBorrow
{
    partial class HisTreatmentBorrowDelete : BusinessBase
    {
        internal HisTreatmentBorrowDelete()
            : base()
        {

        }

        internal HisTreatmentBorrowDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_TREATMENT_BORROW data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTreatmentBorrowCheck checker = new HisTreatmentBorrowCheck(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT_BORROW raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisTreatmentBorrowDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_TREATMENT_BORROW> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTreatmentBorrowCheck checker = new HisTreatmentBorrowCheck(param);
                List<HIS_TREATMENT_BORROW> listRaw = new List<HIS_TREATMENT_BORROW>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisTreatmentBorrowDAO.DeleteList(listData);
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
