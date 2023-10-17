using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAtc
{
    partial class HisAtcUpdate : BusinessBase
    {
		private List<HIS_ATC> beforeUpdateHisAtcs = new List<HIS_ATC>();
		
        internal HisAtcUpdate()
            : base()
        {

        }

        internal HisAtcUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_ATC data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAtcCheck checker = new HisAtcCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_ATC raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.ATC_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisAtcDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAtc_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAtc that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisAtcs.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_ATC> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAtcCheck checker = new HisAtcCheck(param);
                List<HIS_ATC> listRaw = new List<HIS_ATC>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.ATC_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisAtcDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAtc_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAtc that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisAtcs.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisAtcs))
            {
                if (!DAOWorker.HisAtcDAO.UpdateList(this.beforeUpdateHisAtcs))
                {
                    LogSystem.Warn("Rollback du lieu HisAtc that bai, can kiem tra lai." + LogUtil.TraceData("HisAtcs", this.beforeUpdateHisAtcs));
                }
				this.beforeUpdateHisAtcs = null;
            }
        }
    }
}
