using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisServiceMety
{
    partial class HisServiceMetyTruncate : BusinessBase
    {
        internal HisServiceMetyTruncate()
            : base()
        {

        }

        internal HisServiceMetyTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_SERVICE_METY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceMetyCheck checker = new HisServiceMetyCheck(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_METY raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisServiceMetyDAO.Truncate(data);
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
                HisServiceMetyCheck checker = new HisServiceMetyCheck(param);
                valid = valid && IsNotNullOrEmpty(ids);
                List<HIS_SERVICE_METY> listRaw = new List<HIS_SERVICE_METY>();
                valid = valid && checker.VerifyIds(ids, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisServiceMetyDAO.TruncateList(listRaw);
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

        internal bool TruncateList(List<HIS_SERVICE_METY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisServiceMetyCheck checker = new HisServiceMetyCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisServiceMetyDAO.TruncateList(listData);
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
                List<HIS_SERVICE_METY> hisServices = new HisServiceMetyGet().GetByServiceId(anticipateId);
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
