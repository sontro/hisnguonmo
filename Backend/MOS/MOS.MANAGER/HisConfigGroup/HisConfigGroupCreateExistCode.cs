using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisConfigGroup
{
    partial class HisConfigGroupCreate : BusinessBase
    {
		private List<HIS_CONFIG_GROUP> recentHisConfigGroups = new List<HIS_CONFIG_GROUP>();
		
        internal HisConfigGroupCreate()
            : base()
        {

        }

        internal HisConfigGroupCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_CONFIG_GROUP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisConfigGroupCheck checker = new HisConfigGroupCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.CONFIG_GROUP_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisConfigGroupDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisConfigGroup_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisConfigGroup that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisConfigGroups.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisConfigGroups))
            {
                if (!DAOWorker.HisConfigGroupDAO.TruncateList(this.recentHisConfigGroups))
                {
                    LogSystem.Warn("Rollback du lieu HisConfigGroup that bai, can kiem tra lai." + LogUtil.TraceData("recentHisConfigGroups", this.recentHisConfigGroups));
                }
				this.recentHisConfigGroups = null;
            }
        }
    }
}
