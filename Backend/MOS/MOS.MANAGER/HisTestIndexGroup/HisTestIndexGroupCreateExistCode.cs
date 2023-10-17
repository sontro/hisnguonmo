using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTestIndexGroup
{
    partial class HisTestIndexGroupCreate : BusinessBase
    {
		private List<HIS_TEST_INDEX_GROUP> recentHisTestIndexGroups = new List<HIS_TEST_INDEX_GROUP>();
		
        internal HisTestIndexGroupCreate()
            : base()
        {

        }

        internal HisTestIndexGroupCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_TEST_INDEX_GROUP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTestIndexGroupCheck checker = new HisTestIndexGroupCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.TEST_INDEX_GROUP_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisTestIndexGroupDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTestIndexGroup_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisTestIndexGroup that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisTestIndexGroups.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisTestIndexGroups))
            {
                if (!DAOWorker.HisTestIndexGroupDAO.TruncateList(this.recentHisTestIndexGroups))
                {
                    LogSystem.Warn("Rollback du lieu HisTestIndexGroup that bai, can kiem tra lai." + LogUtil.TraceData("recentHisTestIndexGroups", this.recentHisTestIndexGroups));
                }
				this.recentHisTestIndexGroups = null;
            }
        }
    }
}
