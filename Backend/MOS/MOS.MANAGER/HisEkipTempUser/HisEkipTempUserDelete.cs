using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisEkipTempUser
{
    partial class HisEkipTempUserDelete : BusinessBase
    {
        internal HisEkipTempUserDelete()
            : base()
        {

        }

        internal HisEkipTempUserDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_EKIP_TEMP_USER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEkipTempUserCheck checker = new HisEkipTempUserCheck(param);
                valid = valid && IsNotNull(data);
                HIS_EKIP_TEMP_USER raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisEkipTempUserDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_EKIP_TEMP_USER> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisEkipTempUserCheck checker = new HisEkipTempUserCheck(param);
                List<HIS_EKIP_TEMP_USER> listRaw = new List<HIS_EKIP_TEMP_USER>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisEkipTempUserDAO.DeleteList(listData);
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
