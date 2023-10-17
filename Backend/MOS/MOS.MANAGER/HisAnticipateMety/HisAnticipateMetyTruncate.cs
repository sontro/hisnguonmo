using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAnticipateMety
{
    partial class HisAnticipateMetyTruncate : BusinessBase
    {
        internal HisAnticipateMetyTruncate()
            : base()
        {

        }

        internal HisAnticipateMetyTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_ANTICIPATE_METY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAnticipateMetyCheck checker = new HisAnticipateMetyCheck(param);
                valid = valid && IsNotNull(data);
                HIS_ANTICIPATE_METY raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisAnticipateMetyDAO.Truncate(data);
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

        internal bool TruncateList(List<HIS_ANTICIPATE_METY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAnticipateMetyCheck checker = new HisAnticipateMetyCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisAnticipateMetyDAO.TruncateList(listData);
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
                List<HIS_ANTICIPATE_METY> hisAnticipates = new HisAnticipateMetyGet().GetByAnticipateId(anticipateId);
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
