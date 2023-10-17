using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBedBsty
{
    partial class HisBedBstyCreate : BusinessBase
    {
		private List<HIS_BED_BSTY> recentHisBedBstys = new List<HIS_BED_BSTY>();
		
        internal HisBedBstyCreate()
            : base()
        {

        }

        internal HisBedBstyCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_BED_BSTY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBedBstyCheck checker = new HisBedBstyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.BED_BSTY_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisBedBstyDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBedBsty_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisBedBsty that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisBedBstys.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisBedBstys))
            {
                if (!new HisBedBstyTruncate(param).TruncateList(this.recentHisBedBstys))
                {
                    LogSystem.Warn("Rollback du lieu HisBedBsty that bai, can kiem tra lai." + LogUtil.TraceData("recentHisBedBstys", this.recentHisBedBstys));
                }
            }
        }
    }
}
