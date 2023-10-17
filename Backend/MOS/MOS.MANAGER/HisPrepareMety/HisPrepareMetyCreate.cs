using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPrepareMety
{
    partial class HisPrepareMetyCreate : BusinessBase
    {
		private List<HIS_PREPARE_METY> recentHisPrepareMetys = new List<HIS_PREPARE_METY>();
		
        internal HisPrepareMetyCreate()
            : base()
        {

        }

        internal HisPrepareMetyCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_PREPARE_METY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPrepareMetyCheck checker = new HisPrepareMetyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisPrepareMetyDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPrepareMety_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisPrepareMety that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisPrepareMetys.Add(data);
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
		
		internal bool CreateList(List<HIS_PREPARE_METY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPrepareMetyCheck checker = new HisPrepareMetyCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisPrepareMetyDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPrepareMety_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisPrepareMety that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisPrepareMetys.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisPrepareMetys))
            {
                if (!DAOWorker.HisPrepareMetyDAO.TruncateList(this.recentHisPrepareMetys))
                {
                    LogSystem.Warn("Rollback du lieu HisPrepareMety that bai, can kiem tra lai." + LogUtil.TraceData("recentHisPrepareMetys", this.recentHisPrepareMetys));
                }
				this.recentHisPrepareMetys = null;
            }
        }
    }
}
