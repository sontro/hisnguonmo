using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRestRetrType;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisRehaTrainType
{
    partial class HisRehaTrainTypeTruncate : BusinessBase
    {
        internal HisRehaTrainTypeTruncate()
            : base()
        {

        }

        internal HisRehaTrainTypeTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_REHA_TRAIN_TYPE data)
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
                valid = valid && checker.CheckConstraint(data.ID);
                if (valid)
                {
                    if (!new HisRestRetrTypeTruncate(param).TruncateByRehaTrainTypeId(raw.ID))
                    {
                        throw new Exception("Xoa du lieu HIS_REST_RETR_TYPE that bai. Ket thuc nghiep vu");
                    }

                    result = DAOWorker.HisRehaTrainTypeDAO.Truncate(data);
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

        internal bool TruncateList(List<HIS_REHA_TRAIN_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisRehaTrainTypeCheck checker = new HisRehaTrainTypeCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisRehaTrainTypeDAO.TruncateList(listData);
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
