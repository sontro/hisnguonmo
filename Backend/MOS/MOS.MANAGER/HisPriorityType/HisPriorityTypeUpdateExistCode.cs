using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisPriorityType
{
    partial class HisPriorityTypeUpdate : BusinessBase
    {
		private List<HIS_PRIORITY_TYPE> beforeUpdateHisPriorityTypes = new List<HIS_PRIORITY_TYPE>();
		
        internal HisPriorityTypeUpdate()
            : base()
        {

        }

        internal HisPriorityTypeUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_PRIORITY_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPriorityTypeCheck checker = new HisPriorityTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_PRIORITY_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.PRIORITY_TYPE_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisPriorityTypeDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPriorityType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPriorityType that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisPriorityTypes.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_PRIORITY_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPriorityTypeCheck checker = new HisPriorityTypeCheck(param);
                List<HIS_PRIORITY_TYPE> listRaw = new List<HIS_PRIORITY_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.PRIORITY_TYPE_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisPriorityTypeDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPriorityType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPriorityType that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisPriorityTypes.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisPriorityTypes))
            {
                if (!DAOWorker.HisPriorityTypeDAO.UpdateList(this.beforeUpdateHisPriorityTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisPriorityType that bai, can kiem tra lai." + LogUtil.TraceData("HisPriorityTypes", this.beforeUpdateHisPriorityTypes));
                }
				this.beforeUpdateHisPriorityTypes = null;
            }
        }
    }
}
