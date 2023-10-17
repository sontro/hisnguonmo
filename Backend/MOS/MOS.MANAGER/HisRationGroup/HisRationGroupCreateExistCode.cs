using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRationGroup
{
    partial class HisRationGroupCreate : BusinessBase
    {
		private List<HIS_RATION_GROUP> recentHisRationGroups = new List<HIS_RATION_GROUP>();
		
        internal HisRationGroupCreate()
            : base()
        {

        }

        internal HisRationGroupCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_RATION_GROUP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRationGroupCheck checker = new HisRationGroupCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.RATION_GROUP_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisRationGroupDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRationGroup_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisRationGroup that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisRationGroups.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisRationGroups))
            {
                if (!DAOWorker.HisRationGroupDAO.TruncateList(this.recentHisRationGroups))
                {
                    LogSystem.Warn("Rollback du lieu HisRationGroup that bai, can kiem tra lai." + LogUtil.TraceData("recentHisRationGroups", this.recentHisRationGroups));
                }
				this.recentHisRationGroups = null;
            }
        }
    }
}
