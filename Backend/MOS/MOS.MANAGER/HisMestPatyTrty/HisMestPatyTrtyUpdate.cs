using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMestPatyTrty
{
    partial class HisMestPatyTrtyUpdate : BusinessBase
    {
		private List<HIS_MEST_PATY_TRTY> beforeUpdateHisMestPatyTrtys = new List<HIS_MEST_PATY_TRTY>();
		
        internal HisMestPatyTrtyUpdate()
            : base()
        {

        }

        internal HisMestPatyTrtyUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_MEST_PATY_TRTY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMestPatyTrtyCheck checker = new HisMestPatyTrtyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_MEST_PATY_TRTY raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.IsNotExists(data);
                if (valid)
                {                    
					if (!DAOWorker.HisMestPatyTrtyDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMestPatyTrty_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMestPatyTrty that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisMestPatyTrtys.Add(raw);
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

        internal bool UpdateList(List<HIS_MEST_PATY_TRTY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMestPatyTrtyCheck checker = new HisMestPatyTrtyCheck(param);
                List<HIS_MEST_PATY_TRTY> listRaw = new List<HIS_MEST_PATY_TRTY>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.IsNotExists(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisMestPatyTrtyDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMestPatyTrty_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMestPatyTrty that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisMestPatyTrtys.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisMestPatyTrtys))
            {
                if (!DAOWorker.HisMestPatyTrtyDAO.UpdateList(this.beforeUpdateHisMestPatyTrtys))
                {
                    LogSystem.Warn("Rollback du lieu HisMestPatyTrty that bai, can kiem tra lai." + LogUtil.TraceData("HisMestPatyTrtys", this.beforeUpdateHisMestPatyTrtys));
                }
				this.beforeUpdateHisMestPatyTrtys = null;
            }
        }
    }
}
