using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisPosition
{
    partial class HisPositionUpdate : BusinessBase
    {
		private List<HIS_POSITION> beforeUpdateHisPositions = new List<HIS_POSITION>();
		
        internal HisPositionUpdate()
            : base()
        {

        }

        internal HisPositionUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_POSITION data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPositionCheck checker = new HisPositionCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_POSITION raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisPositionDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPosition_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPosition that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisPositions.Add(raw);
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

        internal bool UpdateList(List<HIS_POSITION> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPositionCheck checker = new HisPositionCheck(param);
                List<HIS_POSITION> listRaw = new List<HIS_POSITION>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisPositionDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPosition_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPosition that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisPositions.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisPositions))
            {
                if (!DAOWorker.HisPositionDAO.UpdateList(this.beforeUpdateHisPositions))
                {
                    LogSystem.Warn("Rollback du lieu HisPosition that bai, can kiem tra lai." + LogUtil.TraceData("HisPositions", this.beforeUpdateHisPositions));
                }
				this.beforeUpdateHisPositions = null;
            }
        }
    }
}
