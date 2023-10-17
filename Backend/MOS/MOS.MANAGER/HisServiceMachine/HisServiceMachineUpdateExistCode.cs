using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisServiceMachine
{
    partial class HisServiceMachineUpdate : BusinessBase
    {
		private List<HIS_SERVICE_MACHINE> beforeUpdateHisServiceMachines = new List<HIS_SERVICE_MACHINE>();
		
        internal HisServiceMachineUpdate()
            : base()
        {

        }

        internal HisServiceMachineUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_SERVICE_MACHINE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceMachineCheck checker = new HisServiceMachineCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_SERVICE_MACHINE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.SERVICE_MACHINE_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisServiceMachineDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceMachine_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisServiceMachine that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisServiceMachines.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_SERVICE_MACHINE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisServiceMachineCheck checker = new HisServiceMachineCheck(param);
                List<HIS_SERVICE_MACHINE> listRaw = new List<HIS_SERVICE_MACHINE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.SERVICE_MACHINE_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisServiceMachineDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceMachine_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisServiceMachine that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisServiceMachines.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisServiceMachines))
            {
                if (!DAOWorker.HisServiceMachineDAO.UpdateList(this.beforeUpdateHisServiceMachines))
                {
                    LogSystem.Warn("Rollback du lieu HisServiceMachine that bai, can kiem tra lai." + LogUtil.TraceData("HisServiceMachines", this.beforeUpdateHisServiceMachines));
                }
				this.beforeUpdateHisServiceMachines = null;
            }
        }
    }
}
