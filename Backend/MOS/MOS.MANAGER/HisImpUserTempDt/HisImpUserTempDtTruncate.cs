using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRestRetrType;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisImpUserTempDt
{
    partial class HisImpUserTempDtTruncate : BusinessBase
    {
        internal HisImpUserTempDtTruncate()
            : base()
        {

        }

        internal HisImpUserTempDtTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(long id)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisImpUserTempDtCheck checker = new HisImpUserTempDtCheck(param);
                HIS_IMP_USER_TEMP_DT raw = null;
                valid = valid && checker.VerifyId(id, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(id);
                if (valid)
                {
                    result = DAOWorker.HisImpUserTempDtDAO.Truncate(raw);
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

        internal bool TruncateList(List<HIS_IMP_USER_TEMP_DT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisImpUserTempDtCheck checker = new HisImpUserTempDtCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
					valid = valid && checker.CheckConstraint(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisImpUserTempDtDAO.TruncateList(listData);
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

        internal bool TruncateByImpUserTempId(long impUserTempId)
        {
            bool result = true;
            try
            {
                List<HIS_IMP_USER_TEMP_DT> impUserTemps = new HisImpUserTempDtGet().GetByImpUserTempId(impUserTempId);
                if (IsNotNullOrEmpty(impUserTemps))
                {
                    result = this.TruncateList(impUserTemps);
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
