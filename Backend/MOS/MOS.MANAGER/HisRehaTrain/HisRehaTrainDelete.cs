using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRehaTrain
{
    class HisRehaTrainDelete : BusinessBase
    {
        internal HisRehaTrainDelete()
            : base()
        {

        }

        internal HisRehaTrainDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(long id)
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
                    result = DAOWorker.HisRehaTrainDAO.Delete(raw);
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

        internal bool Delete(HIS_REHA_TRAIN data)
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
                    result = DAOWorker.HisRehaTrainDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_REHA_TRAIN> listData)
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
                    result = DAOWorker.HisRehaTrainDAO.DeleteList(listData);
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

        internal bool DeleteBySereServRehaId(long id)
        {
            bool result = true;
            try
            {
                List<HIS_REHA_TRAIN> hisRehaTrains = new HisRehaTrainGet().GetBySereServRehaId(id);
                if (IsNotNullOrEmpty(hisRehaTrains))
                {
                    result = this.DeleteList(hisRehaTrains);
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

        internal bool DeleteBySereServRehaIds(List<long> ids)
        {
            bool result = true;
            try
            {
                List<HIS_REHA_TRAIN> hisRehaTrains = new HisRehaTrainGet().GetBySereServRehaIds(ids);
                if (IsNotNullOrEmpty(hisRehaTrains))
                {
                    result = this.DeleteList(hisRehaTrains);
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
