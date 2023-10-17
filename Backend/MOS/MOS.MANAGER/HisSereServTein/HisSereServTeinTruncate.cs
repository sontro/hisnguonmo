using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServTein
{
    class HisSereServTeinTruncate : BusinessBase
    {
        internal HisSereServTeinTruncate()
            : base()
        {

        }

        internal HisSereServTeinTruncate(Inventec.Core.CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_SERE_SERV_TEIN data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSereServTeinCheck checker = new HisSereServTeinCheck(param);
                valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                if (valid)
                {
                    result = DAOWorker.HisSereServTeinDAO.Truncate(data);
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

        internal bool TruncateList(List<HIS_SERE_SERV_TEIN> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSereServTeinCheck checker = new HisSereServTeinCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisSereServTeinDAO.TruncateList(listData);
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

        internal bool TruncateBySereServIds(List<long> ids)
        {
            bool result = false;
            List<HIS_SERE_SERV_TEIN> listData = new HisSereServTeinGet().GetBySereServIds(ids);
            if (IsNotNullOrEmpty(listData))
            {
                result = this.TruncateList(listData); 
            }
            return result;
        }
    }
}
