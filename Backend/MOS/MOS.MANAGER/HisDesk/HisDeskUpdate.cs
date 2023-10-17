using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisDesk
{
    partial class HisDeskUpdate : BusinessBase
    {
		private List<HIS_DESK> beforeUpdateHisDesks = new List<HIS_DESK>();
		
        internal HisDeskUpdate()
            : base()
        {

        }

        internal HisDeskUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_DESK data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDeskCheck checker = new HisDeskCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_DESK raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisDeskDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDesk_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisDesk that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisDesks.Add(raw);
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

        internal bool UpdateList(List<HIS_DESK> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisDeskCheck checker = new HisDeskCheck(param);
                List<HIS_DESK> listRaw = new List<HIS_DESK>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisDeskDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDesk_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisDesk that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisDesks.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisDesks))
            {
                if (!DAOWorker.HisDeskDAO.UpdateList(this.beforeUpdateHisDesks))
                {
                    LogSystem.Warn("Rollback du lieu HisDesk that bai, can kiem tra lai." + LogUtil.TraceData("HisDesks", this.beforeUpdateHisDesks));
                }
				this.beforeUpdateHisDesks = null;
            }
        }
    }
}
