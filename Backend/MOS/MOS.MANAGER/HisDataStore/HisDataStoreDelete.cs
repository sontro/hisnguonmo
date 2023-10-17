using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDataStore
{
    class HisDataStoreDelete : BusinessBase
    {
        internal HisDataStoreDelete()
            : base()
        {

        }

        internal HisDataStoreDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_DATA_STORE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDataStoreCheck checker = new HisDataStoreCheck(param);
                valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                if (valid)
                {
                    result = DAOWorker.HisDataStoreDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_DATA_STORE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisDataStoreCheck checker = new HisDataStoreCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisDataStoreDAO.DeleteList(listData);
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
