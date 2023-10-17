using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMachineServMaty
{
    partial class HisMachineServMatyCreate : BusinessBase
    {
		private List<HIS_MACHINE_SERV_MATY> recentHisMachineServMatys = new List<HIS_MACHINE_SERV_MATY>();
		
        internal HisMachineServMatyCreate()
            : base()
        {

        }

        internal HisMachineServMatyCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MACHINE_SERV_MATY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMachineServMatyCheck checker = new HisMachineServMatyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisMachineServMatyDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMachineServMaty_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMachineServMaty that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisMachineServMatys.Add(data);
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
		
		internal bool CreateList(List<HIS_MACHINE_SERV_MATY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMachineServMatyCheck checker = new HisMachineServMatyCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMachineServMatyDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMachineServMaty_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMachineServMaty that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisMachineServMatys.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisMachineServMatys))
            {
                if (!DAOWorker.HisMachineServMatyDAO.TruncateList(this.recentHisMachineServMatys))
                {
                    LogSystem.Warn("Rollback du lieu HisMachineServMaty that bai, can kiem tra lai." + LogUtil.TraceData("recentHisMachineServMatys", this.recentHisMachineServMatys));
                }
				this.recentHisMachineServMatys = null;
            }
        }
    }
}
