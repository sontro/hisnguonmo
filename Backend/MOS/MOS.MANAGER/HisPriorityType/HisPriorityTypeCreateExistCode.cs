using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPriorityType
{
    partial class HisPriorityTypeCreate : BusinessBase
    {
		private List<HIS_PRIORITY_TYPE> recentHisPriorityTypes = new List<HIS_PRIORITY_TYPE>();
		
        internal HisPriorityTypeCreate()
            : base()
        {

        }

        internal HisPriorityTypeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_PRIORITY_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPriorityTypeCheck checker = new HisPriorityTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.PRIORITY_TYPE_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisPriorityTypeDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPriorityType_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisPriorityType that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisPriorityTypes.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisPriorityTypes))
            {
                if (!DAOWorker.HisPriorityTypeDAO.TruncateList(this.recentHisPriorityTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisPriorityType that bai, can kiem tra lai." + LogUtil.TraceData("recentHisPriorityTypes", this.recentHisPriorityTypes));
                }
				this.recentHisPriorityTypes = null;
            }
        }
    }
}
