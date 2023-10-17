using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBank
{
    partial class HisBankUpdate : BusinessBase
    {
		private List<HIS_BANK> beforeUpdateHisBanks = new List<HIS_BANK>();
		
        internal HisBankUpdate()
            : base()
        {

        }

        internal HisBankUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_BANK data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBankCheck checker = new HisBankCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_BANK raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.BANK_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisBankDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBank_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBank that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisBanks.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_BANK> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBankCheck checker = new HisBankCheck(param);
                List<HIS_BANK> listRaw = new List<HIS_BANK>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.BANK_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisBankDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBank_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBank that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisBanks.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisBanks))
            {
                if (!DAOWorker.HisBankDAO.UpdateList(this.beforeUpdateHisBanks))
                {
                    LogSystem.Warn("Rollback du lieu HisBank that bai, can kiem tra lai." + LogUtil.TraceData("HisBanks", this.beforeUpdateHisBanks));
                }
				this.beforeUpdateHisBanks = null;
            }
        }
    }
}
