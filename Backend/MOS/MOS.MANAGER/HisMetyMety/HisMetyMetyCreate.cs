using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMetyMety
{
    partial class HisMetyMetyCreate : BusinessBase
    {
		private List<HIS_METY_METY> recentHisMetyMetys = new List<HIS_METY_METY>();
		
        internal HisMetyMetyCreate()
            : base()
        {

        }

        internal HisMetyMetyCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_METY_METY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMetyMetyCheck checker = new HisMetyMetyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisMetyMetyDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMetyMety_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMetyMety that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisMetyMetys.Add(data);
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
		
		internal bool CreateList(List<HIS_METY_METY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMetyMetyCheck checker = new HisMetyMetyCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMetyMetyDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMetyMety_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMetyMety that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisMetyMetys.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisMetyMetys))
            {
                if (!DAOWorker.HisMetyMetyDAO.TruncateList(this.recentHisMetyMetys))
                {
                    LogSystem.Warn("Rollback du lieu HisMetyMety that bai, can kiem tra lai." + LogUtil.TraceData("recentHisMetyMetys", this.recentHisMetyMetys));
                }
				this.recentHisMetyMetys = null;
            }
        }
    }
}
