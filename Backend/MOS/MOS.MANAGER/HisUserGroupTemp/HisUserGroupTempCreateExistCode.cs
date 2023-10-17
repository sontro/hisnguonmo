using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisUserGroupTemp
{
    partial class HisUserGroupTempCreate : BusinessBase
    {
		private List<HIS_USER_GROUP_TEMP> recentHisUserGroupTemps = new List<HIS_USER_GROUP_TEMP>();
		
        internal HisUserGroupTempCreate()
            : base()
        {

        }

        internal HisUserGroupTempCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_USER_GROUP_TEMP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisUserGroupTempCheck checker = new HisUserGroupTempCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.USER_GROUP_TEMP_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisUserGroupTempDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisUserGroupTemp_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisUserGroupTemp that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisUserGroupTemps.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisUserGroupTemps))
            {
                if (!DAOWorker.HisUserGroupTempDAO.TruncateList(this.recentHisUserGroupTemps))
                {
                    LogSystem.Warn("Rollback du lieu HisUserGroupTemp that bai, can kiem tra lai." + LogUtil.TraceData("recentHisUserGroupTemps", this.recentHisUserGroupTemps));
                }
				this.recentHisUserGroupTemps = null;
            }
        }
    }
}
