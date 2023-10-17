using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSpeedUnit
{
    partial class HisSpeedUnitUpdate : BusinessBase
    {
		private List<HIS_SPEED_UNIT> beforeUpdateHisSpeedUnits = new List<HIS_SPEED_UNIT>();
		
        internal HisSpeedUnitUpdate()
            : base()
        {

        }

        internal HisSpeedUnitUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_SPEED_UNIT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSpeedUnitCheck checker = new HisSpeedUnitCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_SPEED_UNIT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisSpeedUnitDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSpeedUnit_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSpeedUnit that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisSpeedUnits.Add(raw);
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

        internal bool UpdateList(List<HIS_SPEED_UNIT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSpeedUnitCheck checker = new HisSpeedUnitCheck(param);
                List<HIS_SPEED_UNIT> listRaw = new List<HIS_SPEED_UNIT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisSpeedUnitDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSpeedUnit_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSpeedUnit that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisSpeedUnits.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisSpeedUnits))
            {
                if (!DAOWorker.HisSpeedUnitDAO.UpdateList(this.beforeUpdateHisSpeedUnits))
                {
                    LogSystem.Warn("Rollback du lieu HisSpeedUnit that bai, can kiem tra lai." + LogUtil.TraceData("HisSpeedUnits", this.beforeUpdateHisSpeedUnits));
                }
				this.beforeUpdateHisSpeedUnits = null;
            }
        }
    }
}
