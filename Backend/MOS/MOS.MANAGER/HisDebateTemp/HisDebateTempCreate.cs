using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDebateTemp
{
    partial class HisDebateTempCreate : BusinessBase
    {
		private List<HIS_DEBATE_TEMP> recentHisDebateTemps = new List<HIS_DEBATE_TEMP>();
		
        internal HisDebateTempCreate()
            : base()
        {

        }

        internal HisDebateTempCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_DEBATE_TEMP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDebateTempCheck checker = new HisDebateTempCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisDebateTempDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDebateTemp_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisDebateTemp that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisDebateTemps.Add(data);
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
		
		internal bool CreateList(List<HIS_DEBATE_TEMP> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisDebateTempCheck checker = new HisDebateTempCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisDebateTempDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDebateTemp_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisDebateTemp that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisDebateTemps.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisDebateTemps))
            {
                if (!DAOWorker.HisDebateTempDAO.TruncateList(this.recentHisDebateTemps))
                {
                    LogSystem.Warn("Rollback du lieu HisDebateTemp that bai, can kiem tra lai." + LogUtil.TraceData("recentHisDebateTemps", this.recentHisDebateTemps));
                }
				this.recentHisDebateTemps = null;
            }
        }
    }
}
