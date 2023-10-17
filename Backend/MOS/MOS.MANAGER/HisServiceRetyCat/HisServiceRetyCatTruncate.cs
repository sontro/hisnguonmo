using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisServiceRetyCat
{
    partial class HisServiceRetyCatTruncate : BusinessBase
    {
        internal HisServiceRetyCatTruncate()
            : base()
        {

        }

        internal HisServiceRetyCatTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_SERVICE_RETY_CAT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceRetyCatCheck checker = new HisServiceRetyCatCheck(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_RETY_CAT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisServiceRetyCatDAO.Truncate(data);
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

        internal bool TruncateList(List<HIS_SERVICE_RETY_CAT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisServiceRetyCatCheck checker = new HisServiceRetyCatCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisServiceRetyCatDAO.TruncateList(listData);
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
                HisServiceRetyCatCheck checker = new HisServiceRetyCatCheck(param);
                List<HIS_SERVICE_RETY_CAT> listRaw = new List<HIS_SERVICE_RETY_CAT>();
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
        internal bool TruncateByReportTypeCatId(long id)
        {
            bool result = true;
            try
            {
                List<HIS_SERVICE_RETY_CAT> hisServiceRetyCats = new HisServiceRetyCatGet().GetByReportTypeCatId(id);
                if (IsNotNullOrEmpty(hisServiceRetyCats))
                {
                    result = this.TruncateList(hisServiceRetyCats);
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
