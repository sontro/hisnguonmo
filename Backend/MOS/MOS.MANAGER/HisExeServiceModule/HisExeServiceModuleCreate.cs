using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExeServiceModule
{
    partial class HisExeServiceModuleCreate : BusinessBase
    {
		private List<HIS_EXE_SERVICE_MODULE> recentHisExeServiceModules = new List<HIS_EXE_SERVICE_MODULE>();
		
        internal HisExeServiceModuleCreate()
            : base()
        {

        }

        internal HisExeServiceModuleCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_EXE_SERVICE_MODULE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExeServiceModuleCheck checker = new HisExeServiceModuleCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisExeServiceModuleDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExeServiceModule_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisExeServiceModule that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisExeServiceModules.Add(data);
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
		
		internal bool CreateList(List<HIS_EXE_SERVICE_MODULE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExeServiceModuleCheck checker = new HisExeServiceModuleCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisExeServiceModuleDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExeServiceModule_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisExeServiceModule that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisExeServiceModules.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisExeServiceModules))
            {
                if (!DAOWorker.HisExeServiceModuleDAO.TruncateList(this.recentHisExeServiceModules))
                {
                    LogSystem.Warn("Rollback du lieu HisExeServiceModule that bai, can kiem tra lai." + LogUtil.TraceData("recentHisExeServiceModules", this.recentHisExeServiceModules));
                }
				this.recentHisExeServiceModules = null;
            }
        }
    }
}
