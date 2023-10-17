using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRestRetrType;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBedBsty
{
    partial class HisBedBstyTruncate : BusinessBase
    {
        internal HisBedBstyTruncate()
            : base()
        {

        }

        internal HisBedBstyTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(long id)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBedBstyCheck checker = new HisBedBstyCheck(param);
                HIS_BED_BSTY raw = null;
                valid = valid && checker.VerifyId(id, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(id);
                if (valid)
                {
                    result = DAOWorker.HisBedBstyDAO.Truncate(raw);
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

        internal bool TruncateList(List<HIS_BED_BSTY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBedBstyCheck checker = new HisBedBstyCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
					valid = valid && checker.CheckConstraint(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisBedBstyDAO.TruncateList(listData);
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

        internal bool TruncateList(List<long> ids)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(ids);
                HisBedBstyCheck checker = new HisBedBstyCheck(param);
                List<HIS_BED_BSTY> listRaw = new List<HIS_BED_BSTY>();
                valid = valid && checker.VerifyIds(ids, listRaw);
                if (valid)
                {
                    result = this.TruncateList(listRaw);
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
