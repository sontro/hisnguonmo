using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRestRetrType;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTransfusion
{
    partial class HisTransfusionTruncate : BusinessBase
    {
        internal HisTransfusionTruncate()
            : base()
        {

        }

        internal HisTransfusionTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(long id)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTransfusionCheck checker = new HisTransfusionCheck(param);
                HIS_TRANSFUSION raw = null;
                valid = valid && checker.VerifyId(id, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.IsCreatorOrAdmin(raw);
                valid = valid && checker.VefifyTreatment(raw);
                valid = valid && checker.CheckConstraint(id);
                if (valid)
                {
                    result = DAOWorker.HisTransfusionDAO.Truncate(raw);
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

        internal bool TruncateList(List<HIS_TRANSFUSION> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTransfusionCheck checker = new HisTransfusionCheck(param);
                foreach (var data in listData)
                {
                    HIS_TRANSFUSION raw = null;
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.VerifyId(data.ID, ref raw);
                    valid = valid && checker.IsUnLock(data.ID);
					valid = valid && checker.CheckConstraint(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisTransfusionDAO.TruncateList(listData);
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

        internal bool TruncateByTransfusionSumId(long transfusionSumId)
        {
            bool result = true;
            try
            {
                List<HIS_TRANSFUSION> listData = new HisTransfusionGet().GetByTransfusionSumId(transfusionSumId);
                if (IsNotNullOrEmpty(listData))
                {
                    result = this.TruncateList(listData);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
    }
}
