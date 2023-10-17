using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRestRetrType;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAnticipateBlty
{
    partial class HisAnticipateBltyTruncate : BusinessBase
    {
        internal HisAnticipateBltyTruncate()
            : base()
        {

        }

        internal HisAnticipateBltyTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(long id)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAnticipateBltyCheck checker = new HisAnticipateBltyCheck(param);
                HIS_ANTICIPATE_BLTY raw = null;
                valid = valid && checker.VerifyId(id, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(id);
                if (valid)
                {
                    result = DAOWorker.HisAnticipateBltyDAO.Truncate(raw);
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

        internal bool TruncateList(List<HIS_ANTICIPATE_BLTY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAnticipateBltyCheck checker = new HisAnticipateBltyCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                    valid = valid && checker.CheckConstraint(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisAnticipateBltyDAO.TruncateList(listData);
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

        internal bool TruncateByAnticipateId(long anticipateId)
        {
            bool result = false;
            try
            {
                List<HIS_ANTICIPATE_BLTY> hisAnticipates = new HisAnticipateBltyGet().GetByAnticipateId(anticipateId);
                if (IsNotNullOrEmpty(hisAnticipates))
                {
                    result = this.TruncateList(hisAnticipates);
                }
                else
                {
                    result = true;
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
