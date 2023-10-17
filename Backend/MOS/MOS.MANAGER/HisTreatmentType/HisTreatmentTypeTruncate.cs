using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTreatmentType
{
    partial class HisTreatmentTypeTruncate : BusinessBase
    {
        internal HisTreatmentTypeTruncate()
            : base()
        {

        }

        internal HisTreatmentTypeTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_TREATMENT_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTreatmentTypeCheck checker = new HisTreatmentTypeCheck(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(data.ID);
                if (valid)
                {
                    result = DAOWorker.HisTreatmentTypeDAO.Truncate(data);
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
