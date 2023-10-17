using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisServiceSame
{
    partial class HisServiceSameUpdate : BusinessBase
    {
		private List<HIS_SERVICE_SAME> beforeUpdateHisServiceSames = new List<HIS_SERVICE_SAME>();
		
        internal HisServiceSameUpdate()
            : base()
        {

        }

        internal HisServiceSameUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_SERVICE_SAME data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceSameCheck checker = new HisServiceSameCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_SERVICE_SAME raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisServiceSameDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceSame_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisServiceSame that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisServiceSames.Add(raw);
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

        internal bool UpdateList(List<HIS_SERVICE_SAME> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisServiceSameCheck checker = new HisServiceSameCheck(param);
                List<HIS_SERVICE_SAME> listRaw = new List<HIS_SERVICE_SAME>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisServiceSameDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceSame_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisServiceSame that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisServiceSames.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisServiceSames))
            {
                if (!DAOWorker.HisServiceSameDAO.UpdateList(this.beforeUpdateHisServiceSames))
                {
                    LogSystem.Warn("Rollback du lieu HisServiceSame that bai, can kiem tra lai." + LogUtil.TraceData("HisServiceSames", this.beforeUpdateHisServiceSames));
                }
				this.beforeUpdateHisServiceSames = null;
            }
        }
    }
}
