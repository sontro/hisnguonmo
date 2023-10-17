using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMachineServMaty
{
    partial class HisMachineServMatyUpdate : BusinessBase
    {
		private List<HIS_MACHINE_SERV_MATY> beforeUpdateHisMachineServMatys = new List<HIS_MACHINE_SERV_MATY>();
		
        internal HisMachineServMatyUpdate()
            : base()
        {

        }

        internal HisMachineServMatyUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_MACHINE_SERV_MATY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMachineServMatyCheck checker = new HisMachineServMatyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_MACHINE_SERV_MATY raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisMachineServMatyDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMachineServMaty_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMachineServMaty that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisMachineServMatys.Add(raw);
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

        internal bool UpdateList(List<HIS_MACHINE_SERV_MATY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMachineServMatyCheck checker = new HisMachineServMatyCheck(param);
                List<HIS_MACHINE_SERV_MATY> listRaw = new List<HIS_MACHINE_SERV_MATY>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisMachineServMatyDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMachineServMaty_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMachineServMaty that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisMachineServMatys.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisMachineServMatys))
            {
                if (!DAOWorker.HisMachineServMatyDAO.UpdateList(this.beforeUpdateHisMachineServMatys))
                {
                    LogSystem.Warn("Rollback du lieu HisMachineServMaty that bai, can kiem tra lai." + LogUtil.TraceData("HisMachineServMatys", this.beforeUpdateHisMachineServMatys));
                }
				this.beforeUpdateHisMachineServMatys = null;
            }
        }
    }
}
