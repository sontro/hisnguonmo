using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisHoreDhty
{
    partial class HisHoreDhtyCreate : BusinessBase
    {
		private List<HIS_HORE_DHTY> recentHisHoreDhtys = new List<HIS_HORE_DHTY>();
		
        internal HisHoreDhtyCreate()
            : base()
        {

        }

        internal HisHoreDhtyCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_HORE_DHTY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisHoreDhtyCheck checker = new HisHoreDhtyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisHoreDhtyDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisHoreDhty_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisHoreDhty that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisHoreDhtys.Add(data);
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
		
		internal bool CreateList(List<HIS_HORE_DHTY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisHoreDhtyCheck checker = new HisHoreDhtyCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisHoreDhtyDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisHoreDhty_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisHoreDhty that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisHoreDhtys.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisHoreDhtys))
            {
                if (!DAOWorker.HisHoreDhtyDAO.TruncateList(this.recentHisHoreDhtys))
                {
                    LogSystem.Warn("Rollback du lieu HisHoreDhty that bai, can kiem tra lai." + LogUtil.TraceData("recentHisHoreDhtys", this.recentHisHoreDhtys));
                }
				this.recentHisHoreDhtys = null;
            }
        }
    }
}
