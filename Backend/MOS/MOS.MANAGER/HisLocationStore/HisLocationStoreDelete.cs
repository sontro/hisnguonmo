using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisLocationStore
{
    partial class HisLocationStoreDelete : BusinessBase
    {
        internal HisLocationStoreDelete()
            : base()
        {

        }

        internal HisLocationStoreDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_LOCATION_STORE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisLocationStoreCheck checker = new HisLocationStoreCheck(param);
                valid = valid && IsNotNull(data);
                HIS_LOCATION_STORE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisLocationStoreDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_LOCATION_STORE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisLocationStoreCheck checker = new HisLocationStoreCheck(param);
                List<HIS_LOCATION_STORE> listRaw = new List<HIS_LOCATION_STORE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisLocationStoreDAO.DeleteList(listData);
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
