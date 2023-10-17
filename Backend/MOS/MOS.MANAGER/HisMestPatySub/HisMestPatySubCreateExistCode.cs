using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestPatySub
{
    partial class HisMestPatySubCreate : BusinessBase
    {
		private List<HIS_MEST_PATY_SUB> recentHisMestPatySubs = new List<HIS_MEST_PATY_SUB>();
		
        internal HisMestPatySubCreate()
            : base()
        {

        }

        internal HisMestPatySubCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MEST_PATY_SUB data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMestPatySubCheck checker = new HisMestPatySubCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.MEST_PATY_SUB_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisMestPatySubDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMestPatySub_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMestPatySub that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisMestPatySubs.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisMestPatySubs))
            {
                if (!new HisMestPatySubTruncate(param).TruncateList(this.recentHisMestPatySubs))
                {
                    LogSystem.Warn("Rollback du lieu HisMestPatySub that bai, can kiem tra lai." + LogUtil.TraceData("recentHisMestPatySubs", this.recentHisMestPatySubs));
                }
            }
        }
    }
}
