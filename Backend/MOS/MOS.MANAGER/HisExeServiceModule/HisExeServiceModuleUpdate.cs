using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExeServiceModule
{
    partial class HisExeServiceModuleUpdate : BusinessBase
    {
		private List<HIS_EXE_SERVICE_MODULE> beforeUpdateHisExeServiceModules = new List<HIS_EXE_SERVICE_MODULE>();
		
        internal HisExeServiceModuleUpdate()
            : base()
        {

        }

        internal HisExeServiceModuleUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_EXE_SERVICE_MODULE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExeServiceModuleCheck checker = new HisExeServiceModuleCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_EXE_SERVICE_MODULE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisExeServiceModuleDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExeServiceModule_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisExeServiceModule that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisExeServiceModules.Add(raw);
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

        internal bool UpdateList(List<HIS_EXE_SERVICE_MODULE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExeServiceModuleCheck checker = new HisExeServiceModuleCheck(param);
                List<HIS_EXE_SERVICE_MODULE> listRaw = new List<HIS_EXE_SERVICE_MODULE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisExeServiceModuleDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExeServiceModule_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisExeServiceModule that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisExeServiceModules.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisExeServiceModules))
            {
                if (!DAOWorker.HisExeServiceModuleDAO.UpdateList(this.beforeUpdateHisExeServiceModules))
                {
                    LogSystem.Warn("Rollback du lieu HisExeServiceModule that bai, can kiem tra lai." + LogUtil.TraceData("HisExeServiceModules", this.beforeUpdateHisExeServiceModules));
                }
				this.beforeUpdateHisExeServiceModules = null;
            }
        }
    }
}
