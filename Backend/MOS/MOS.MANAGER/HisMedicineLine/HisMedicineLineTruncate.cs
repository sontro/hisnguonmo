using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMedicineLine
{
    partial class HisMedicineLineTruncate : BusinessBase
    {
        internal HisMedicineLineTruncate()
            : base()
        {

        }

        internal HisMedicineLineTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_MEDICINE_LINE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMedicineLineCheck checker = new HisMedicineLineCheck(param);
                valid = valid && IsNotNull(data);
                HIS_MEDICINE_LINE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(raw);
                if (valid)
                {
                    result = DAOWorker.HisMedicineLineDAO.Truncate(data);
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
