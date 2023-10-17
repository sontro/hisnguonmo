using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisRestRetrType
{
    partial class HisRestRetrTypeTruncate : BusinessBase
    {
        internal HisRestRetrTypeTruncate()
            : base()
        {

        }

        internal HisRestRetrTypeTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_REST_RETR_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRestRetrTypeCheck checker = new HisRestRetrTypeCheck(param);
                valid = valid && IsNotNull(data);
                HIS_REST_RETR_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisRestRetrTypeDAO.Truncate(data);
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

        internal bool TruncateList(List<HIS_REST_RETR_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisRestRetrTypeCheck checker = new HisRestRetrTypeCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisRestRetrTypeDAO.TruncateList(listData);
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

        internal bool TruncateList(List<long> Ids)
        {
            bool result = false;
            try
            {
                bool valid = true;
                List<HIS_REST_RETR_TYPE> listRaw = new List<HIS_REST_RETR_TYPE>();
                valid = IsNotNullOrEmpty(Ids);
                HisRestRetrTypeCheck checker = new HisRestRetrTypeCheck(param);
                checker.VerifyIds(Ids, listRaw);
                foreach (var data in listRaw)
                {
                    valid = valid && checker.IsUnLock(data);
                }
                if (valid)
                {
                    result = DAOWorker.HisRestRetrTypeDAO.TruncateList(listRaw);
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

        internal bool TruncateByRehaTrainTypeId(long id)
        {
            bool result = true;
            try
            {
                List<HIS_REST_RETR_TYPE> hisRestRetrTypes = new HisRestRetrTypeGet().GetByRehaTrainTypeId(id);
                if (IsNotNullOrEmpty(hisRestRetrTypes))
                {
                    result = this.TruncateList(hisRestRetrTypes);
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

        internal bool TruncateByRehaServiceTypeId(long id)
        {
            bool result = true;
            try
            {
                List<HIS_REST_RETR_TYPE> hisRestRetrTypes = new HisRestRetrTypeGet().GetByRehaServiceTypeId(id);
                if (IsNotNullOrEmpty(hisRestRetrTypes))
                {
                    result = this.TruncateList(hisRestRetrTypes);
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
