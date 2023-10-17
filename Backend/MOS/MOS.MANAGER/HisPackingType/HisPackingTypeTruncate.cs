using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisPackingType
{
    partial class HisPackingTypeTruncate : BusinessBase
    {
        internal HisPackingTypeTruncate()
            : base()
        {

        }

        internal HisPackingTypeTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_PACKING_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPackingTypeCheck checker = new HisPackingTypeCheck(param);
                valid = valid && IsNotNull(data);
                HIS_PACKING_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(raw);
                if (valid)
                {
                    result = DAOWorker.HisPackingTypeDAO.Truncate(data);
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
