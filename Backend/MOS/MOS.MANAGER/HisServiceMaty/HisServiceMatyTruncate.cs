using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisServiceMaty
{
    partial class HisServiceMatyTruncate : BusinessBase
    {
        internal HisServiceMatyTruncate()
            : base()
        {

        }

        internal HisServiceMatyTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_SERVICE_MATY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceMatyCheck checker = new HisServiceMatyCheck(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_MATY raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisServiceMatyDAO.Truncate(data);
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
                HisServiceMatyCheck checker = new HisServiceMatyCheck(param);
                valid = valid && IsNotNullOrEmpty(ids);
                List<HIS_SERVICE_MATY> listRaw = new List<HIS_SERVICE_MATY>();
                valid = valid && checker.VerifyIds(ids, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisServiceMatyDAO.TruncateList(listRaw);
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

        internal bool TruncateList(List<HIS_SERVICE_MATY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisServiceMatyCheck checker = new HisServiceMatyCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisServiceMatyDAO.TruncateList(listData);
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

        internal bool TruncateByServiceId(long anticipateId)
        {
            bool result = false;
            try
            {
                List<HIS_SERVICE_MATY> hisServices = new HisServiceMatyGet().GetByServiceId(anticipateId);
                if (IsNotNullOrEmpty(hisServices))
                {
                    result = this.TruncateList(hisServices);
                }
                result = true;
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
