using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceMachine
{
    partial class HisServiceMachineCreate : BusinessBase
    {
		private List<HIS_SERVICE_MACHINE> recentHisServiceMachines = new List<HIS_SERVICE_MACHINE>();
		
        internal HisServiceMachineCreate()
            : base()
        {

        }

        internal HisServiceMachineCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_SERVICE_MACHINE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceMachineCheck checker = new HisServiceMachineCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisServiceMachineDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceMachine_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisServiceMachine that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisServiceMachines.Add(data);
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
		
		internal bool CreateList(List<HIS_SERVICE_MACHINE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisServiceMachineCheck checker = new HisServiceMachineCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisServiceMachineDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceMachine_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisServiceMachine that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisServiceMachines.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisServiceMachines))
            {
                if (!DAOWorker.HisServiceMachineDAO.TruncateList(this.recentHisServiceMachines))
                {
                    LogSystem.Warn("Rollback du lieu HisServiceMachine that bai, can kiem tra lai." + LogUtil.TraceData("recentHisServiceMachines", this.recentHisServiceMachines));
                }
				this.recentHisServiceMachines = null;
            }
        }
    }
}
