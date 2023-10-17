using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisCarerCard
{
    partial class HisCarerCardCancelLost : BusinessBase
    {
        private List<HIS_CARER_CARD> beforeUpdateHisCarerCards = new List<HIS_CARER_CARD>();

        internal HisCarerCardCancelLost()
            : base()
        {
        }

        internal HisCarerCardCancelLost(CommonParam paramUpdate)
            : base(paramUpdate)
        {
        }

        internal bool Run(long id)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisCarerCardCheck checker = new HisCarerCardCheck(param);
                HIS_CARER_CARD raw = null;
                valid = valid && checker.VerifyId(id, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    raw.IS_LOST = null;
                    if (!DAOWorker.HisCarerCardDAO.Update(raw))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCarerCard_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisCarerCard that bai." + LogUtil.TraceData("data", raw));
                    }
                    this.beforeUpdateHisCarerCards.Add(raw);
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

        internal void RollbackData()
        {
            if (IsNotNullOrEmpty(this.beforeUpdateHisCarerCards))
            {
                if (!DAOWorker.HisCarerCardDAO.UpdateList(this.beforeUpdateHisCarerCards))
                {
                    LogSystem.Warn("Rollback du lieu HisCarerCard that bai, can kiem tra lai." + LogUtil.TraceData("HisCarerCards", this.beforeUpdateHisCarerCards));
                }
                this.beforeUpdateHisCarerCards = null;
            }
        }
    }
}
