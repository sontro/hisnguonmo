using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMachine
{
    partial class HisMachineUpdate : BusinessBase
    {
		private List<HIS_MACHINE> beforeUpdateHisMachines = new List<HIS_MACHINE>();
		
        internal HisMachineUpdate()
            : base()
        {

        }

        internal HisMachineUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_MACHINE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMachineCheck checker = new HisMachineCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_MACHINE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.MACHINE_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisMachineDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMachine_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMachine that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisMachines.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_MACHINE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMachineCheck checker = new HisMachineCheck(param);
                List<HIS_MACHINE> listRaw = new List<HIS_MACHINE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.MACHINE_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisMachineDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMachine_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMachine that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisMachines.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisMachines))
            {
                if (!DAOWorker.HisMachineDAO.UpdateList(this.beforeUpdateHisMachines))
                {
                    LogSystem.Warn("Rollback du lieu HisMachine that bai, can kiem tra lai." + LogUtil.TraceData("HisMachines", this.beforeUpdateHisMachines));
                }
				this.beforeUpdateHisMachines = null;
            }
        }
    }
}
