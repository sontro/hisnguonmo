using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBornPosition
{
    partial class HisBornPositionUpdate : BusinessBase
    {
		private List<HIS_BORN_POSITION> beforeUpdateHisBornPositions = new List<HIS_BORN_POSITION>();
		
        internal HisBornPositionUpdate()
            : base()
        {

        }

        internal HisBornPositionUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_BORN_POSITION data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBornPositionCheck checker = new HisBornPositionCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_BORN_POSITION raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.BORN_POSITION_CODE, data.ID);
                if (valid)
                {
                    this.beforeUpdateHisBornPositions.Add(raw);
					if (!DAOWorker.HisBornPositionDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBornPosition_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBornPosition that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_BORN_POSITION> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBornPositionCheck checker = new HisBornPositionCheck(param);
                List<HIS_BORN_POSITION> listRaw = new List<HIS_BORN_POSITION>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.BORN_POSITION_CODE, data.ID);
                }
                if (valid)
                {
					this.beforeUpdateHisBornPositions.AddRange(listRaw);
					if (!DAOWorker.HisBornPositionDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBornPosition_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBornPosition that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisBornPositions))
            {
                if (!new HisBornPositionUpdate(param).UpdateList(this.beforeUpdateHisBornPositions))
                {
                    LogSystem.Warn("Rollback du lieu HisBornPosition that bai, can kiem tra lai." + LogUtil.TraceData("HisBornPositions", this.beforeUpdateHisBornPositions));
                }
            }
        }
    }
}
