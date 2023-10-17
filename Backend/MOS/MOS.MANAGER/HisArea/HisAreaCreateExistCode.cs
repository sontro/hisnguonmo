using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisArea
{
    partial class HisAreaCreate : BusinessBase
    {
		private List<HIS_AREA> recentHisAreas = new List<HIS_AREA>();
		
        internal HisAreaCreate()
            : base()
        {

        }

        internal HisAreaCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_AREA data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAreaCheck checker = new HisAreaCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.AREA_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisAreaDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisArea_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisArea that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisAreas.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisAreas))
            {
                if (!DAOWorker.HisAreaDAO.TruncateList(this.recentHisAreas))
                {
                    LogSystem.Warn("Rollback du lieu HisArea that bai, can kiem tra lai." + LogUtil.TraceData("recentHisAreas", this.recentHisAreas));
                }
				this.recentHisAreas = null;
            }
        }
    }
}
