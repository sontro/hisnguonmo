using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPrepareMaty
{
    partial class HisPrepareMatyCreate : BusinessBase
    {
		private List<HIS_PREPARE_MATY> recentHisPrepareMatys = new List<HIS_PREPARE_MATY>();
		
        internal HisPrepareMatyCreate()
            : base()
        {

        }

        internal HisPrepareMatyCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_PREPARE_MATY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPrepareMatyCheck checker = new HisPrepareMatyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisPrepareMatyDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPrepareMaty_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisPrepareMaty that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisPrepareMatys.Add(data);
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
		
		internal bool CreateList(List<HIS_PREPARE_MATY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPrepareMatyCheck checker = new HisPrepareMatyCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisPrepareMatyDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPrepareMaty_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisPrepareMaty that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisPrepareMatys.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisPrepareMatys))
            {
                if (!DAOWorker.HisPrepareMatyDAO.TruncateList(this.recentHisPrepareMatys))
                {
                    LogSystem.Warn("Rollback du lieu HisPrepareMaty that bai, can kiem tra lai." + LogUtil.TraceData("recentHisPrepareMatys", this.recentHisPrepareMatys));
                }
				this.recentHisPrepareMatys = null;
            }
        }
    }
}
