using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisHoldReturn
{
    partial class HisHoldReturnCreate : BusinessBase
    {
		private List<HIS_HOLD_RETURN> recentHisHoldReturns = new List<HIS_HOLD_RETURN>();
		
        internal HisHoldReturnCreate()
            : base()
        {
        }

        internal HisHoldReturnCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_HOLD_RETURN data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisHoldReturnCheck checker = new HisHoldReturnCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisHoldReturnDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisHoldReturn_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisHoldReturn that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisHoldReturns.Add(data);
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
		
		internal bool CreateList(List<HIS_HOLD_RETURN> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisHoldReturnCheck checker = new HisHoldReturnCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisHoldReturnDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisHoldReturn_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisHoldReturn that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisHoldReturns.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisHoldReturns))
            {
                if (!DAOWorker.HisHoldReturnDAO.TruncateList(this.recentHisHoldReturns))
                {
                    LogSystem.Warn("Rollback du lieu HisHoldReturn that bai, can kiem tra lai." + LogUtil.TraceData("recentHisHoldReturns", this.recentHisHoldReturns));
                }
				this.recentHisHoldReturns = null;
            }
        }
    }
}
