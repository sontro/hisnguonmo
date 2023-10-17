using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEmrCoverConfig
{
    partial class HisEmrCoverConfigCreate : BusinessBase
    {
		private List<HIS_EMR_COVER_CONFIG> recentHisEmrCoverConfigs = new List<HIS_EMR_COVER_CONFIG>();
		
        internal HisEmrCoverConfigCreate()
            : base()
        {

        }

        internal HisEmrCoverConfigCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_EMR_COVER_CONFIG data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEmrCoverConfigCheck checker = new HisEmrCoverConfigCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.IsNotExists(data);
                if (valid)
                {
					if (!DAOWorker.HisEmrCoverConfigDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEmrCoverConfig_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisEmrCoverConfig that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisEmrCoverConfigs.Add(data);
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
		
		internal bool CreateList(List<HIS_EMR_COVER_CONFIG> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisEmrCoverConfigCheck checker = new HisEmrCoverConfigCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.IsNotExists(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisEmrCoverConfigDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEmrCoverConfig_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisEmrCoverConfig that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisEmrCoverConfigs.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisEmrCoverConfigs))
            {
                if (!DAOWorker.HisEmrCoverConfigDAO.TruncateList(this.recentHisEmrCoverConfigs))
                {
                    LogSystem.Warn("Rollback du lieu HisEmrCoverConfig that bai, can kiem tra lai." + LogUtil.TraceData("recentHisEmrCoverConfigs", this.recentHisEmrCoverConfigs));
                }
				this.recentHisEmrCoverConfigs = null;
            }
        }
    }
}
