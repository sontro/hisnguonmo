using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExpBltyService
{
    partial class HisExpBltyServiceDelete : BusinessBase
    {
        internal HisExpBltyServiceDelete()
            : base()
        {

        }

        internal HisExpBltyServiceDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_EXP_BLTY_SERVICE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExpBltyServiceCheck checker = new HisExpBltyServiceCheck(param);
                valid = valid && IsNotNull(data);
                HIS_EXP_BLTY_SERVICE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisExpBltyServiceDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_EXP_BLTY_SERVICE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExpBltyServiceCheck checker = new HisExpBltyServiceCheck(param);
                List<HIS_EXP_BLTY_SERVICE> listRaw = new List<HIS_EXP_BLTY_SERVICE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisExpBltyServiceDAO.DeleteList(listData);
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
