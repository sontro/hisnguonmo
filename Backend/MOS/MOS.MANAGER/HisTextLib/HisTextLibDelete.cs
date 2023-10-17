using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTextLib
{
    class HisTextLibDelete : BusinessBase
    {
        internal HisTextLibDelete()
            : base()
        {

        }

        internal HisTextLibDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_TEXT_LIB data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTextLibCheck checker = new HisTextLibCheck(param);
                valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                if (valid)
                {
                    result = DAOWorker.HisTextLibDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_TEXT_LIB> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTextLibCheck checker = new HisTextLibCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisTextLibDAO.DeleteList(listData);
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
