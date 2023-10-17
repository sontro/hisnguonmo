using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisProgram
{
    partial class HisProgramCreate : BusinessBase
    {
		private List<HIS_PROGRAM> recentHisPrograms = new List<HIS_PROGRAM>();
		
        internal HisProgramCreate()
            : base()
        {

        }

        internal HisProgramCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_PROGRAM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisProgramCheck checker = new HisProgramCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisProgramDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisProgram_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisProgram that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisPrograms.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisPrograms))
            {
                if (!new HisProgramTruncate(param).TruncateList(this.recentHisPrograms))
                {
                    LogSystem.Warn("Rollback du lieu HisProgram that bai, can kiem tra lai." + LogUtil.TraceData("recentHisPrograms", this.recentHisPrograms));
                }
            }
        }
    }
}
