using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRestRetrType;
using MOS.MANAGER.HisTransfusion;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTransfusionSum
{
    partial class HisTransfusionSumTruncate : BusinessBase
    {
        internal HisTransfusionSumTruncate()
            : base()
        {

        }

        internal HisTransfusionSumTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(long id)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTransfusionSumCheck checker = new HisTransfusionSumCheck(param);
                HIS_TRANSFUSION_SUM raw = null;
                valid = valid && checker.VerifyId(id, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.IsCreatorOrAdmin(raw);
                valid = valid && checker.VefifyTreatment(raw);                
                valid = valid && checker.CheckConstraint(id);
                if (valid)
                {
                    if (new HisTransfusionTruncate(param).TruncateByTransfusionSumId(raw.ID))
                    {
                        result = DAOWorker.HisTransfusionSumDAO.Truncate(raw);
                    }
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

        internal bool TruncateList(List<HIS_TRANSFUSION_SUM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTransfusionSumCheck checker = new HisTransfusionSumCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                    valid = valid && checker.CheckConstraint(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisTransfusionSumDAO.TruncateList(listData);
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
