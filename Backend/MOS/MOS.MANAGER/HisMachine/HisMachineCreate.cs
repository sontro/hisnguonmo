using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMachine
{
    partial class HisMachineCreate : BusinessBase
    {
		private List<HIS_MACHINE> recentHisMachines = new List<HIS_MACHINE>();
		
        internal HisMachineCreate()
            : base()
        {

        }

        internal HisMachineCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MACHINE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMachineCheck checker = new HisMachineCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisMachineDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMachine_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMachine that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisMachines.Add(data);
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
		
		internal bool CreateList(List<HIS_MACHINE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMachineCheck checker = new HisMachineCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMachineDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMachine_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMachine that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisMachines.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisMachines))
            {
                if (!DAOWorker.HisMachineDAO.TruncateList(this.recentHisMachines))
                {
                    LogSystem.Warn("Rollback du lieu HisMachine that bai, can kiem tra lai." + LogUtil.TraceData("recentHisMachines", this.recentHisMachines));
                }
				this.recentHisMachines = null;
            }
        }
    }
}
