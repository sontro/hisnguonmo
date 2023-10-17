using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAdr
{
    partial class HisAdrUpdate : BusinessBase
    {
		private List<HIS_ADR> beforeUpdateHisAdrs = new List<HIS_ADR>();
		
        internal HisAdrUpdate()
            : base()
        {

        }

        internal HisAdrUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_ADR data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAdrCheck checker = new HisAdrCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_ADR raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisAdrDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAdr_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAdr that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisAdrs.Add(raw);
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

        internal bool Update(HIS_ADR data, HIS_ADR before)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAdrCheck checker = new HisAdrCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.IsUnLock(before);
                if (valid)
                {
                    if (!DAOWorker.HisAdrDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAdr_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAdr that bai." + LogUtil.TraceData("data", data));
                    }
                    this.beforeUpdateHisAdrs.Add(before);
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

        internal bool UpdateList(List<HIS_ADR> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAdrCheck checker = new HisAdrCheck(param);
                List<HIS_ADR> listRaw = new List<HIS_ADR>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisAdrDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAdr_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAdr that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisAdrs.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisAdrs))
            {
                if (!DAOWorker.HisAdrDAO.UpdateList(this.beforeUpdateHisAdrs))
                {
                    LogSystem.Warn("Rollback du lieu HisAdr that bai, can kiem tra lai." + LogUtil.TraceData("HisAdrs", this.beforeUpdateHisAdrs));
                }
				this.beforeUpdateHisAdrs = null;
            }
        }
    }
}
