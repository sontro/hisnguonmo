using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPosition
{
    partial class HisPositionCreate : BusinessBase
    {
		private List<HIS_POSITION> recentHisPositions = new List<HIS_POSITION>();
		
        internal HisPositionCreate()
            : base()
        {

        }

        internal HisPositionCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_POSITION data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPositionCheck checker = new HisPositionCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisPositionDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPosition_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisPosition that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisPositions.Add(data);
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
		
		internal bool CreateList(List<HIS_POSITION> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPositionCheck checker = new HisPositionCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisPositionDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPosition_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisPosition that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisPositions.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisPositions))
            {
                if (!DAOWorker.HisPositionDAO.TruncateList(this.recentHisPositions))
                {
                    LogSystem.Warn("Rollback du lieu HisPosition that bai, can kiem tra lai." + LogUtil.TraceData("recentHisPositions", this.recentHisPositions));
                }
				this.recentHisPositions = null;
            }
        }
    }
}
