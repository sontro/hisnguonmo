using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPetroleum
{
    partial class HisPetroleumCreate : BusinessBase
    {
		private List<HIS_PETROLEUM> recentHisPetroleums = new List<HIS_PETROLEUM>();
		
        internal HisPetroleumCreate()
            : base()
        {

        }

        internal HisPetroleumCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_PETROLEUM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPetroleumCheck checker = new HisPetroleumCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.PETROLEUM_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisPetroleumDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPetroleum_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisPetroleum that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisPetroleums.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisPetroleums))
            {
                if (!DAOWorker.HisPetroleumDAO.TruncateList(this.recentHisPetroleums))
                {
                    LogSystem.Warn("Rollback du lieu HisPetroleum that bai, can kiem tra lai." + LogUtil.TraceData("recentHisPetroleums", this.recentHisPetroleums));
                }
				this.recentHisPetroleums = null;
            }
        }
    }
}
