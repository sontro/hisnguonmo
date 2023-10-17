using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisCareDetail
{
    partial class HisCareDetailTruncate : BusinessBase
    {
        internal HisCareDetailTruncate()
            : base()
        {

        }

        internal HisCareDetailTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_CARE_DETAIL data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisCareDetailCheck checker = new HisCareDetailCheck(param);
                valid = valid && IsNotNull(data);
                HIS_CARE_DETAIL raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisCareDetailDAO.Truncate(data);
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

        internal bool TruncateList(List<HIS_CARE_DETAIL> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisCareDetailCheck checker = new HisCareDetailCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisCareDetailDAO.TruncateList(listData);
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

        internal bool TruncateByCareId(long id)
        {
            bool result = true;
            try
            {
                List<HIS_CARE_DETAIL> hisCareDetails = new HisCareDetailGet().GetByCareId(id);
                if (IsNotNullOrEmpty(hisCareDetails))
                {
                    result = this.TruncateList(hisCareDetails);
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
