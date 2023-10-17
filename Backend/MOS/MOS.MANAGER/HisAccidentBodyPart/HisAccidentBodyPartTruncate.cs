using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRestRetrType;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAccidentBodyPart
{
    partial class HisAccidentBodyPartTruncate : BusinessBase
    {
        internal HisAccidentBodyPartTruncate()
            : base()
        {

        }

        internal HisAccidentBodyPartTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_ACCIDENT_BODY_PART data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAccidentBodyPartCheck checker = new HisAccidentBodyPartCheck(param);
                valid = valid && IsNotNull(data);
                HIS_ACCIDENT_BODY_PART raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(data.ID);
                if (valid)
                {
                    result = DAOWorker.HisAccidentBodyPartDAO.Truncate(data);
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

        internal bool TruncateList(List<HIS_ACCIDENT_BODY_PART> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAccidentBodyPartCheck checker = new HisAccidentBodyPartCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisAccidentBodyPartDAO.TruncateList(listData);
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
