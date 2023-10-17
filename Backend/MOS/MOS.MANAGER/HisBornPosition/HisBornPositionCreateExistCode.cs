using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBornPosition
{
    partial class HisBornPositionCreate : BusinessBase
    {
		private List<HIS_BORN_POSITION> recentHisBornPositions = new List<HIS_BORN_POSITION>();
		
        internal HisBornPositionCreate()
            : base()
        {

        }

        internal HisBornPositionCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_BORN_POSITION data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBornPositionCheck checker = new HisBornPositionCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.BORN_POSITION_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisBornPositionDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBornPosition_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisBornPosition that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisBornPositions.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisBornPositions))
            {
                if (!new HisBornPositionTruncate(param).TruncateList(this.recentHisBornPositions))
                {
                    LogSystem.Warn("Rollback du lieu HisBornPosition that bai, can kiem tra lai." + LogUtil.TraceData("recentHisBornPositions", this.recentHisBornPositions));
                }
            }
        }
    }
}
