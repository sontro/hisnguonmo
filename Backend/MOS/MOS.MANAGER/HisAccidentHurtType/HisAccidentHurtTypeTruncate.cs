using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAccidentHurtType
{
    partial class HisAccidentHurtTypeTruncate : BusinessBase
    {
        internal HisAccidentHurtTypeTruncate()
            : base()
        {

        }

        internal HisAccidentHurtTypeTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_ACCIDENT_HURT_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAccidentHurtTypeCheck checker = new HisAccidentHurtTypeCheck(param);
                valid = valid && IsNotNull(data);
                HIS_ACCIDENT_HURT_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(raw.ID);
                if (valid)
                {
                    result = DAOWorker.HisAccidentHurtTypeDAO.Truncate(data);
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

        internal bool TruncateList(List<HIS_ACCIDENT_HURT_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAccidentHurtTypeCheck checker = new HisAccidentHurtTypeCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                    valid = valid && checker.CheckConstraint(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisAccidentHurtTypeDAO.TruncateList(listData);
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
