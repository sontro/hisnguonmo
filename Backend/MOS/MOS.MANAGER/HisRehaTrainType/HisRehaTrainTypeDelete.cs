using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisRehaTrainType
{
    partial class HisRehaTrainTypeDelete : BusinessBase
    {
        internal HisRehaTrainTypeDelete()
            : base()
        {

        }

        internal HisRehaTrainTypeDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_REHA_TRAIN_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRehaTrainTypeCheck checker = new HisRehaTrainTypeCheck(param);
                valid = valid && IsNotNull(data);
                HIS_REHA_TRAIN_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisRehaTrainTypeDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_REHA_TRAIN_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisRehaTrainTypeCheck checker = new HisRehaTrainTypeCheck(param);
                List<HIS_REHA_TRAIN_TYPE> listRaw = new List<HIS_REHA_TRAIN_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisRehaTrainTypeDAO.DeleteList(listData);
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
