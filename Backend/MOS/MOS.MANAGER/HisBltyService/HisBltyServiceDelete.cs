using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBltyService
{
    partial class HisBltyServiceDelete : BusinessBase
    {
        internal HisBltyServiceDelete()
            : base()
        {

        }

        internal HisBltyServiceDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_BLTY_SERVICE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBltyServiceCheck checker = new HisBltyServiceCheck(param);
                valid = valid && IsNotNull(data);
                HIS_BLTY_SERVICE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisBltyServiceDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_BLTY_SERVICE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBltyServiceCheck checker = new HisBltyServiceCheck(param);
                List<HIS_BLTY_SERVICE> listRaw = new List<HIS_BLTY_SERVICE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisBltyServiceDAO.DeleteList(listData);
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
