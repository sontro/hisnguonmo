using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAwareness
{
    partial class HisAwarenessUpdate : BusinessBase
    {
		private List<HIS_AWARENESS> beforeUpdateHisAwarenessDTOs = new List<HIS_AWARENESS>();
		
        internal HisAwarenessUpdate()
            : base()
        {

        }

        internal HisAwarenessUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_AWARENESS data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAwarenessCheck checker = new HisAwarenessCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_AWARENESS raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.AWARENESS_CODE, data.ID);
                if (valid)
                {
					this.beforeUpdateHisAwarenessDTOs.Add(raw);
					if (!DAOWorker.HisAwarenessDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAwareness_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAwareness that bai." + LogUtil.TraceData("data", data));
                    }
                    
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

        internal bool UpdateList(List<HIS_AWARENESS> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAwarenessCheck checker = new HisAwarenessCheck(param);
                List<HIS_AWARENESS> listRaw = new List<HIS_AWARENESS>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.AWARENESS_CODE, data.ID);
                }
                if (valid)
                {
					this.beforeUpdateHisAwarenessDTOs.AddRange(listRaw);
					if (!DAOWorker.HisAwarenessDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAwareness_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAwareness that bai." + LogUtil.TraceData("listData", listData));
                    }
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisAwarenessDTOs))
            {
                if (!new HisAwarenessUpdate(param).UpdateList(this.beforeUpdateHisAwarenessDTOs))
                {
                    LogSystem.Warn("Rollback du lieu HisAwareness that bai, can kiem tra lai." + LogUtil.TraceData("HisAwarenessDTOs", this.beforeUpdateHisAwarenessDTOs));
                }
            }
        }
    }
}
