using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRestRetrType;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisNumOrderBlock
{
    partial class HisNumOrderBlockTruncate : BusinessBase
    {
        internal HisNumOrderBlockTruncate()
            : base()
        {

        }

        internal HisNumOrderBlockTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(long id)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisNumOrderBlockCheck checker = new HisNumOrderBlockCheck(param);
                HIS_NUM_ORDER_BLOCK raw = null;
                valid = valid && checker.VerifyId(id, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(id);
                if (valid)
                {
                    result = DAOWorker.HisNumOrderBlockDAO.Truncate(raw);
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

        internal bool TruncateByRoomTimeId(long id)
        {
            bool result = false;
            try
            {
                HisNumOrderBlockFilterQuery filter = new HisNumOrderBlockFilterQuery();
                filter.ROOM_TIME_ID = id;
                List<HIS_NUM_ORDER_BLOCK> numOrderBlocks = new HisNumOrderBlockGet().Get(filter);

                if (IsNotNullOrEmpty(numOrderBlocks))
                {
                    return DAOWorker.HisNumOrderBlockDAO.TruncateList(numOrderBlocks);
                }
                else
                {
                    return true;
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

        internal bool TruncateList(List<HIS_NUM_ORDER_BLOCK> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisNumOrderBlockCheck checker = new HisNumOrderBlockCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
					valid = valid && checker.CheckConstraint(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisNumOrderBlockDAO.TruncateList(listData);
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
