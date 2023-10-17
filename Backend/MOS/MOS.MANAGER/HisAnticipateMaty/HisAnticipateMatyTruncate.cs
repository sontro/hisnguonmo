using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAnticipateMaty
{
    partial class HisAnticipateMatyTruncate : BusinessBase
    {
        internal HisAnticipateMatyTruncate()
            : base()
        {

        }

        internal HisAnticipateMatyTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_ANTICIPATE_MATY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAnticipateMatyCheck checker = new HisAnticipateMatyCheck(param);
                valid = valid && IsNotNull(data);
                HIS_ANTICIPATE_MATY raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisAnticipateMatyDAO.Truncate(data);
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

        internal bool TruncateList(List<HIS_ANTICIPATE_MATY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAnticipateMatyCheck checker = new HisAnticipateMatyCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisAnticipateMatyDAO.TruncateList(listData);
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
            bool result = true;
            try
            {
                List<HIS_ANTICIPATE_MATY> hisAnticipates = new HisAnticipateMatyGet().GetByAnticipateId(anticipateId);
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
