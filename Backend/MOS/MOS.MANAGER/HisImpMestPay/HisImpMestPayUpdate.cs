using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisImpMestPay
{
    partial class HisImpMestPayUpdate : BusinessBase
    {
		private List<HIS_IMP_MEST_PAY> beforeUpdateHisImpMestPays = new List<HIS_IMP_MEST_PAY>();
		
        internal HisImpMestPayUpdate()
            : base()
        {

        }

        internal HisImpMestPayUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_IMP_MEST_PAY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisImpMestPayCheck checker = new HisImpMestPayCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_IMP_MEST_PAY raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisImpMestPayDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisImpMestPay_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisImpMestPay that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisImpMestPays.Add(raw);
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

        internal bool UpdateList(List<HIS_IMP_MEST_PAY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisImpMestPayCheck checker = new HisImpMestPayCheck(param);
                List<HIS_IMP_MEST_PAY> listRaw = new List<HIS_IMP_MEST_PAY>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisImpMestPayDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisImpMestPay_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisImpMestPay that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisImpMestPays.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisImpMestPays))
            {
                if (!DAOWorker.HisImpMestPayDAO.UpdateList(this.beforeUpdateHisImpMestPays))
                {
                    LogSystem.Warn("Rollback du lieu HisImpMestPay that bai, can kiem tra lai." + LogUtil.TraceData("HisImpMestPays", this.beforeUpdateHisImpMestPays));
                }
				this.beforeUpdateHisImpMestPays = null;
            }
        }
    }
}
