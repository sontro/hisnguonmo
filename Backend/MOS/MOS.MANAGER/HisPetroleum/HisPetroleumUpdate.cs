using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisPetroleum
{
    partial class HisPetroleumUpdate : BusinessBase
    {
		private List<HIS_PETROLEUM> beforeUpdateHisPetroleums = new List<HIS_PETROLEUM>();
		
        internal HisPetroleumUpdate()
            : base()
        {

        }

        internal HisPetroleumUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_PETROLEUM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPetroleumCheck checker = new HisPetroleumCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_PETROLEUM raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.PETROLEUM_CODE, data.ID);
                if (valid)
                {                    
					if (!DAOWorker.HisPetroleumDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPetroleum_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPetroleum that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisPetroleums.Add(raw);
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

        internal bool UpdateList(List<HIS_PETROLEUM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPetroleumCheck checker = new HisPetroleumCheck(param);
                List<HIS_PETROLEUM> listRaw = new List<HIS_PETROLEUM>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.PETROLEUM_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisPetroleumDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPetroleum_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPetroleum that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisPetroleums.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisPetroleums))
            {
                if (!DAOWorker.HisPetroleumDAO.UpdateList(this.beforeUpdateHisPetroleums))
                {
                    LogSystem.Warn("Rollback du lieu HisPetroleum that bai, can kiem tra lai." + LogUtil.TraceData("HisPetroleums", this.beforeUpdateHisPetroleums));
                }
				this.beforeUpdateHisPetroleums = null;
            }
        }
    }
}
