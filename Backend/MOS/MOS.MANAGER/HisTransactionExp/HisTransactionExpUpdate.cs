using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTransactionExp
{
    partial class HisTransactionExpUpdate : BusinessBase
    {
		private List<HIS_TRANSACTION_EXP> beforeUpdateHisTransactionExps = new List<HIS_TRANSACTION_EXP>();
		
        internal HisTransactionExpUpdate()
            : base()
        {

        }

        internal HisTransactionExpUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_TRANSACTION_EXP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTransactionExpCheck checker = new HisTransactionExpCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_TRANSACTION_EXP raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisTransactionExpDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTransactionExp_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTransactionExp that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisTransactionExps.Add(raw);
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

        internal bool UpdateList(List<HIS_TRANSACTION_EXP> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTransactionExpCheck checker = new HisTransactionExpCheck(param);
                List<HIS_TRANSACTION_EXP> listRaw = new List<HIS_TRANSACTION_EXP>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisTransactionExpDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTransactionExp_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTransactionExp that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisTransactionExps.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisTransactionExps))
            {
                if (!DAOWorker.HisTransactionExpDAO.UpdateList(this.beforeUpdateHisTransactionExps))
                {
                    LogSystem.Warn("Rollback du lieu HisTransactionExp that bai, can kiem tra lai." + LogUtil.TraceData("HisTransactionExps", this.beforeUpdateHisTransactionExps));
                }
				this.beforeUpdateHisTransactionExps = null;
            }
        }
    }
}
