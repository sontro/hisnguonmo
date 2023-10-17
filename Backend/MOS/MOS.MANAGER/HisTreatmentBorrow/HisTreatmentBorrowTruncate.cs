using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRestRetrType;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTreatmentBorrow
{
    partial class HisTreatmentBorrowTruncate : BusinessBase
    {
        internal HisTreatmentBorrowTruncate()
            : base()
        {

        }

        internal HisTreatmentBorrowTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(long id)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTreatmentBorrowCheck checker = new HisTreatmentBorrowCheck(param);
                HIS_TREATMENT_BORROW raw = null;
                valid = valid && checker.VerifyId(id, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(id);
                valid = valid && checker.IsNotReceive(raw);
                if (valid)
                {
                    result = DAOWorker.HisTreatmentBorrowDAO.Truncate(raw);
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
