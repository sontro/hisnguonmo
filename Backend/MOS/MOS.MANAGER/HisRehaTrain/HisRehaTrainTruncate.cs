using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRehaTrain
{
    class HisRehaTrainTruncate : BusinessBase
    {
        internal HisRehaTrainTruncate()
            : base()
        {

        }

        internal HisRehaTrainTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(long id)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRehaTrainCheck checker = new HisRehaTrainCheck(param);
                valid = valid && IsGreaterThanZero(id);
                HIS_REHA_TRAIN raw = null;
                valid = valid && checker.VerifyId(id, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisRehaTrainDAO.Truncate(raw);
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

        internal bool Truncate(HIS_REHA_TRAIN data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRehaTrainCheck checker = new HisRehaTrainCheck(param);
                valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                if (valid)
                {
                    result = DAOWorker.HisRehaTrainDAO.Truncate(data);
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

        internal bool TruncateList(List<HIS_REHA_TRAIN> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisRehaTrainCheck checker = new HisRehaTrainCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisRehaTrainDAO.TruncateList(listData);
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

        internal bool TruncateBySereServRehaId(long id)
        {
            bool result = true;
            try
            {
                List<HIS_REHA_TRAIN> hisRehaTrains = new HisRehaTrainGet().GetBySereServRehaId(id);
                if (IsNotNullOrEmpty(hisRehaTrains))
                {
                    result = this.TruncateList(hisRehaTrains);
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

        internal bool TruncateBySereServRehaIds(List<long> ids)
        {
            bool result = true;
            try
            {
                List<HIS_REHA_TRAIN> hisRehaTrains = new HisRehaTrainGet().GetBySereServRehaIds(ids);
                if (IsNotNullOrEmpty(hisRehaTrains))
                {
                    result = this.TruncateList(hisRehaTrains);
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
