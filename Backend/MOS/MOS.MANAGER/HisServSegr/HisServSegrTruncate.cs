using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServSegr
{
    class HisServSegrTruncate : BusinessBase
    {
        internal HisServSegrTruncate()
            : base()
        {

        }

        internal HisServSegrTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_SERV_SEGR data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServSegrCheck checker = new HisServSegrCheck(param);
                valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                if (valid)
                {
                    result = DAOWorker.HisServSegrDAO.Truncate(data);
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

        internal bool TruncateList(List<HIS_SERV_SEGR> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisServSegrCheck checker = new HisServSegrCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisServSegrDAO.TruncateList(listData);
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
                HisServSegrCheck checker = new HisServSegrCheck(param);
                List<HIS_SERV_SEGR> listRaw = new List<HIS_SERV_SEGR>();
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

        internal bool TruncateByServiceId(long serviceId)
        {
            bool result = false;
            try
            {
                List<HIS_SERV_SEGR> servSegrs = new HisServSegrGet().GetByServiceId(serviceId);
                if (IsNotNullOrEmpty(servSegrs))
                {
                    result = this.TruncateList(servSegrs);
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

        internal bool TruncateByServiceGroupId(long serviceGroupId)
        {
            bool result = false;
            try
            {
                List<HIS_SERV_SEGR> servSegrs = new HisServSegrGet().GetByServiceGroupId(serviceGroupId);
                if (IsNotNullOrEmpty(servSegrs))
                {
                    result = this.TruncateList(servSegrs);
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
