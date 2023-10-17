using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAccidentPoison
{
    partial class HisAccidentPoisonCreate : BusinessBase
    {
		private List<HIS_ACCIDENT_POISON> recentHisAccidentPoisons = new List<HIS_ACCIDENT_POISON>();
		
        internal HisAccidentPoisonCreate()
            : base()
        {

        }

        internal HisAccidentPoisonCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_ACCIDENT_POISON data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAccidentPoisonCheck checker = new HisAccidentPoisonCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.ACCIDENT_POISON_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisAccidentPoisonDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAccidentPoison_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisAccidentPoison that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisAccidentPoisons.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisAccidentPoisons))
            {
                if (!new HisAccidentPoisonTruncate(param).TruncateList(this.recentHisAccidentPoisons))
                {
                    LogSystem.Warn("Rollback du lieu HisAccidentPoison that bai, can kiem tra lai." + LogUtil.TraceData("recentHisAccidentPoisons", this.recentHisAccidentPoisons));
                }
            }
        }
    }
}
